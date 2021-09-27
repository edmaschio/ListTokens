using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Token.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        /// <summary>
        /// Retorna lista de atribuições de variáveis das pipelines azure à partir de tokens __#__ encontrados na string
        /// </summary>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        [HttpPost]
        public string Post([FromForm] string jsonFile)
        {
            const string pattern = "__[A-Za-z0-9._-]*?__";

            Regex rgx = new(pattern);

            var ocorr = rgx.Matches(jsonFile);

            var origValues = ocorr.Select(s => s.Value.Replace("__", string.Empty)).Distinct().OrderBy(s => s);

            return string.Join(Environment.NewLine, origValues.Select(s => $"{s}=$({s})"));
        }
    }
}
