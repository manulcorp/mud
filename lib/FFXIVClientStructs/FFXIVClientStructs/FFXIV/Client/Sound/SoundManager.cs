using Thread = FFXIVClientStructs.FFXIV.Client.System.Threading.Thread;

namespace FFXIVClientStructs.FFXIV.Client.Sound;

// Client::Sound::SoundManager:
//   Client::System::Resource::ResourceEventListener
//   Client::System::Threading::Thread
//ctor "E8 ?? ?? ?? ?? EB ?? 48 8B C6 BA ?? ?? ?? ?? 48 89 87 ?? ?? ?? ?? 49 8B CF"
/// <summary>
/// This class is the low level handler for sound related functions and abstracts from the operating system.
/// The functions in this class are not intended by SE to be used directly and do not have proper checks for correct values.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0x1C88)]
public unsafe partial struct SoundManager {
    [FieldOffset(0x0000)] public nint ResourceEventListener;
    [FieldOffset(0x0008)] public Thread Thread;
    [FieldOffset(0x0031)] public bool Disabled;
    [FieldOffset(0x0034)] public float MasterVolume;
    [FieldOffset(0x0038)] public float ActiveVolume;
    [FieldOffset(0x003C)] public fixed float Volume[0x13];
    [FieldOffset(0x0088)] public fixed float UnkVolume2[0x13]; // All are 1.0f
    [FieldOffset(0x00D4)] public fixed float UnkVolume3[0x13]; // All are 1.0f
    [FieldOffset(0x0120)] public fixed float UnkVolume4[0x13]; // All are 1.0f
    [FieldOffset(0x016C)] public fixed bool ChannelMutedArray[0x13];
    [FieldOffset(0x017F)] public fixed bool ChannelAlwaysOn[0x13];
    [Obsolete("Use ChannelAlwaysOn")]
    [FieldOffset(0x017F)]
    public fixed bool ChannelAlwayOn[0x13];

    [FieldOffset(0x01C9)] public bool MasterEnabled;
    [FieldOffset(0x01CA)] public bool IsSoundAlways;
    [FieldOffset(0x01CB)] public bool SoundEnabled;
    [FieldOffset(0x01CC)] public bool WindowInactive;

    [FieldOffset(0x02A0)] public nint CriticalSection;

    [FieldOffset(0x02C8)] public nint EventHandle;

    /// <summary>
    /// Calculates effective volume
    /// </summary>
    /// <param name="channel">The specific sound channel. This is different from the channels visible to the user</param>
    /// <returns>Volume in the ange 0-1</returns>
    [MemberFunction("F3 0F 10 0D ?? ?? ?? ?? 48 63 C2")]
    public partial float GetEffectiveVolume(int channel);

    ///<inheritdoc cref="GetEffectiveVolume(int)"/>
    public float GetEffectiveVolume(SoundChannel channel) => GetEffectiveVolume((int)channel);

    /// <summary>
    /// Sets channel volume
    /// </summary>
    /// <param name="channel">The specific sound channel. This is different from the channels visible to the user</param>
    /// <param name="volume">Volume in the ange 0-1</param>
    /// <param name="p3">unknown</param>
    [MemberFunction("E8 ?? ?? ?? ?? FF C7 D1 EB 75 ?? 48 8B 74 24")]
    public partial void SetVolume(int channel, float volume, int p3 = 100);

    ///<inheritdoc cref="SetVolume(int,float,int)"/>
    public void SetVolume(SoundChannel channel, float volume, int p3 = 100) => SetVolume((int)channel, volume, p3);

    public enum SoundChannel : int {
        Bgm1 = 1,
        Se1 = 2,
        Voice = 3,
        System = 4,
        Env1 = 5,
        Se2 = 6,
        Se3 = 7,
        Bgm2 = 8,
        Bgm3 = 9,
        Env2 = 11,
        Env3 = 12,
        Bgm4 = 16,
        Perform = 17
    }
}
