using Anubis.Application.Common.Mappings;
using Anubis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.Dtos.TeamMember
{
    public class TeamMemberDto : IMapFrom<LU_TeamMember>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
