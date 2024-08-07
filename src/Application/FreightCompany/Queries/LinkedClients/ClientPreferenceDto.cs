using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.FreightCompany.Queries.LinkedClients
{
    public class LinkedClientDetailsDto
    {
        public String Message { get; set; }
        public List<ClientPreferenceDto> ClientPreferenceDto { get; set; }
    }
    public class ClientPreferenceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
