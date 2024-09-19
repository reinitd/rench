using Spectre.Console;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Color = System.Drawing.Color;
using Size = System.Drawing.Size;

namespace Rench;

public partial class Overlay : Form
{
    // Thank you: https://www.youtube.com/watch?v=t1ErGj0YnaM
    // Thank you for the patch: https://pastebin.com/yd1MTHkX

    public const string PROCESS_NAME = "wrenchgame-win64-shipping";
    IntPtr handle = FindWindow("UnrealWindow", null);

    RECT rect;

    public struct RECT
    {
        public int left, top, right, bottom;
    }
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    Graphics g;
    Pen myPen = new Pen(System.Drawing.Color.White);

    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.

    [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
    static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

    // You can also call FindWindow(default(string), lpWindowName) or FindWindow((string)null, lpWindowName)


    // This helper static method is required because the 32-bit version of user32.dll does not contain this API
    // (on any versions of Windows), so linking the method will fail at run-time. The bridge dispatches the request
    // to the correct function (GetWindowLong in 32-bit mode and GetWindowLongPtr in 64-bit mode)
    public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
    {
        if (IntPtr.Size == 8)
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        else
            return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
    }

    [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
    private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
    private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
    public Overlay()
    {
        InitializeComponent();
    }

    private void Overlay_Load(object sender, EventArgs e)
    {
        this.BackColor = System.Drawing.Color.Black;
        this.TransparencyKey = System.Drawing.Color.Black;
        this.TopMost = true;
        this.FormBorderStyle = FormBorderStyle.None;
        this.ShowInTaskbar = false;

        HandleRef handleRef = new HandleRef(this, this.Handle);
        IntPtr initialStyle = GetWindowLongPtr(this.Handle, -20);
        SetWindowLongPtr(handleRef, -20, initialStyle | 0x80000 | 0x20);

        Process[] allProcs = Process.GetProcesses();
        Process proc = allProcs.FirstOrDefault(p => p.ProcessName.ToLower() == PROCESS_NAME);

        if (proc == null)
        {
            Console.WriteLine("Wrench is not running! Run Wrench first then re run Rench.\nPress any key to exit...");
            Console.ReadKey();
            Application.Exit();
            return;
        }

        if (handle == IntPtr.Zero)
        {
            Console.WriteLine("WARNING: Window not found!");
            // Application.Exit();
            // return;
        }

        GetWindowRect(handle, out rect);
        this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
        this.Top = rect.top;
        this.Left = rect.left;

        Console.WriteLine("Right: {0}, Left: {1}, Bottom: {2}, Top: {3}",
            rect.right.ToString(), rect.left.ToString(), rect.bottom.ToString(), rect.top.ToString());
    }

    private void Overlay_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        string s = "Rench Version 0.1 [Alpha]";
        Font font = new Font("Inter", 12);
        Brush brush = new SolidBrush(Color.FromArgb(255, 145, 145, 145));

        SizeF textSize = g.MeasureString(s, font);

        float x = this.ClientSize.Width - textSize.Width - 10;
        float y = 10;

        g.DrawString(s, font, brush, new PointF(x, y));


        string smallText = "Press 'Q' to close Rench";
        Font smallFont = new Font("Inter", 11);
        Brush smallBrush = new SolidBrush(Color.FromArgb(255, 145, 145, 145));

        SizeF smallTextSize = g.MeasureString(smallText, smallFont);

        float smallX = 12;
        float smallY = this.ClientSize.Height - smallTextSize.Height - 10 - textSize.Height - 5;

        g.DrawString(smallText, smallFont, smallBrush, new PointF(smallX, smallY));


        // REMINDER: Only open menu if Wrench is in focus.
        s = "Press 'R' to open the Rench menu";
        font = new Font("Inter", 14);
        brush = new SolidBrush(Color.White);

        textSize = g.MeasureString(s, font);

        x = 10;
        y = this.ClientSize.Height - textSize.Height - 10;

        g.DrawString(s, font, brush, new PointF(x, y));

    }
}
