using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Ietf.iCalendar
{
    public class ParticipationRole:PropertyParameter
    {
        public ParticipationRole(string role):base("ROLE", new PropertyParameterValue(role))
        {
        }

        public ParticipationRole(ParticipationRoles role)
            : this(ParticipationRolesToString(role))
        {
        }

        private static string ParticipationRolesToString(ParticipationRoles role)
        {
            switch(role)
            {
                case ParticipationRoles.Chair:
                    return "CHAIR";
                case ParticipationRoles.RequriedParticipant:
                    return "REQ-PARTICIPANT";
                case ParticipationRoles.OptionalParticipant:
                    return "OPT-PARTICIPANT";
                case ParticipationRoles.NonParticipant:
                    return "OPT-PARTICIPANT";
            }

            return null;
        }

        public static ParticipationRole Default
        {
            get
            {
                return new ParticipationRole("REQ-PARTICIPANT");
            }
        }
    }

    public enum ParticipationRoles
    {
        Chair,
        RequriedParticipant,
        OptionalParticipant,
        NonParticipant
    }
}
