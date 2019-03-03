/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID INGAME_INNIT_SOUND = 3813649501U;
        static const AkUniqueID PLAY_ALARM_TICK_SOUND = 1344795595U;
        static const AkUniqueID PLAY_DETECTION_END = 4207641709U;
        static const AkUniqueID PLAY_DETECTION_START = 1411054566U;
        static const AkUniqueID PLAY_DGA_SFX_GAMEOVER = 4216186185U;
        static const AkUniqueID PLAY_GUIDE_VOICE_IN = 234760877U;
        static const AkUniqueID PLAY_GUIDE_VOICE_OUT_01 = 2215638682U;
        static const AkUniqueID PLAY_GUIDE_VOICE_OUT_FAIL = 3134978043U;
        static const AkUniqueID PLAY_MENU = 1278378707U;
        static const AkUniqueID PLAY_MENU_CLIC = 1982372943U;
        static const AkUniqueID PLAY_WHISTLE = 2693257580U;
        static const AkUniqueID RESET_GAME_PARAMETER_FILTER_ON_RTPC = 937281082U;
        static const AkUniqueID RESET_PLAYBACK_SPEED = 722798962U;
        static const AkUniqueID SET_FILTER_ON_RTPC = 3167714578U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace INFILTRATION
        {
            static const AkUniqueID GROUP = 2650745882U;

            namespace STATE
            {
                static const AkUniqueID LIBRE = 2068852889U;
                static const AkUniqueID REPERE = 2421815328U;
            } // namespace STATE
        } // namespace INFILTRATION

        namespace MENU_PAUSE
        {
            static const AkUniqueID GROUP = 2170009975U;

            namespace STATE
            {
                static const AkUniqueID PAUSE = 3092587493U;
                static const AkUniqueID RESUME = 953277036U;
            } // namespace STATE
        } // namespace MENU_PAUSE

        namespace SALLES
        {
            static const AkUniqueID GROUP = 2571129789U;

            namespace STATE
            {
                static const AkUniqueID OUVERT = 1997789724U;
                static const AkUniqueID RENFERME = 240238997U;
            } // namespace STATE
        } // namespace SALLES

        namespace SOUNDEFFECTS
        {
            static const AkUniqueID GROUP = 3898083304U;

            namespace STATE
            {
                static const AkUniqueID IN = 1752637612U;
                static const AkUniqueID OUT = 645492555U;
            } // namespace STATE
        } // namespace SOUNDEFFECTS

    } // namespace STATES

    namespace SWITCHES
    {
        namespace FAIL_CHECK_ZONE
        {
            static const AkUniqueID GROUP = 3926581785U;

            namespace SWITCH
            {
                static const AkUniqueID CHECK = 4040764971U;
                static const AkUniqueID FAIL = 2596272617U;
            } // namespace SWITCH
        } // namespace FAIL_CHECK_ZONE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID DIST_GUIDE_PLAYER = 704234752U;
        static const AkUniqueID FILTER_ON_RTPC = 3583674237U;
        static const AkUniqueID PLAYBACK_RATE = 1524500807U;
        static const AkUniqueID RPM = 796049864U;
        static const AkUniqueID SLOW_GAMEOVER = 978489641U;
        static const AkUniqueID SS_AIR_FEAR = 1351367891U;
        static const AkUniqueID SS_AIR_FREEFALL = 3002758120U;
        static const AkUniqueID SS_AIR_FURY = 1029930033U;
        static const AkUniqueID SS_AIR_MONTH = 2648548617U;
        static const AkUniqueID SS_AIR_PRESENCE = 3847924954U;
        static const AkUniqueID SS_AIR_RPM = 822163944U;
        static const AkUniqueID SS_AIR_SIZE = 3074696722U;
        static const AkUniqueID SS_AIR_STORM = 3715662592U;
        static const AkUniqueID SS_AIR_TIMEOFDAY = 3203397129U;
        static const AkUniqueID SS_AIR_TURBULENCE = 4160247818U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID PROTOTYPE = 3565403361U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID VOICE = 3170124113U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID RVRB_CLOSE = 2949073560U;
        static const AkUniqueID RVRB_OPEN = 4279841884U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID DEFAULT_MOTION_DEVICE = 4230635974U;
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
