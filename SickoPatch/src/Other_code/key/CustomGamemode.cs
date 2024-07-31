using System;

namespace SickoPatch
{
    public class CustomGamemode
    {
        public virtual void OnVotingComplete(MeetingHud __instance, MeetingHud.VoterState[] states, NetworkedPlayerInfo exiled, bool tie)
        {
            
        }

        internal bool OnCastVote(MeetingHud instance, byte srcPlayerId, byte suspectPlayerId)
        {
            throw new NotImplementedException();
        }

        public static CustomGamemode Instance;
    }
}