using Newtonsoft.Json;

namespace WrenchRealm.Models;
public class Rench {
    [JsonProperty("first_open_time")]
    public long FirstOpenTime { get; set; }
    [JsonProperty("prompted_to_install_gd")]
    public bool? PromptedToInstallGD { get; set; }
    [JsonProperty("is_gd_installed")]
    public bool? IsGDInstalled { get; set; }

    [JsonProperty("gd_realm_path")]
    public string? GDRealmPath { get; set; }

    [JsonProperty("wrench_save_path")]
    public string? WrenchSavePath { get; set; }
}