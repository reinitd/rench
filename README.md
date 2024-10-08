# Rench (WrenchRealm)

Rench is for the game Wrench. The purpose of this application is to allow for easy Realm-like saves for the game. \
Rench currently only supports Wrench Beta on PC, not on the Oculus (unless of course you're using the Oculus from your PC in Steam's VR mode).

Rench uses Google Drive and Google Drive for Desktop to sync the realm(s) across all clients. The file structure should look like this, `RenchRealms` is the folder that is in Google Drive: \
**NOTE:** You shouldn't touch the files directly unless you know what you're doing, you should be doing everything through Rench.
```
RenchRealms
└── Realm Name
    └── Sandbox.sav
```

An example below:
```
RenchRealms
└── CommunitySandbox
│   └── CommunitySandbox.sav
└── TestSandbox
    └── TestSandbox.sav
```

If someone is playing in `CommunitySandbox`, there will be a `CommunitySandbox.lock.json` file present:
```
RenchRealms
└── CommunitySandbox
    └── CommunitySandbox.sav
    └── CommunitySandbox.lock.json
```
Inside the `.lock.json` file contains the user(s) playing and at what time they joined. \
If the `.lock.json` file isn't present (meaning no one is in the realm), then Rench will have you turn on multipler hosting (friends can join) and create a lock file; any user that attempts to join will be asked to join the person playing on that save instead.

### To-do
- [ ] Backup-system
- [ ] Entire thing
- [ ] **Move away from WinForms once everything is done**
