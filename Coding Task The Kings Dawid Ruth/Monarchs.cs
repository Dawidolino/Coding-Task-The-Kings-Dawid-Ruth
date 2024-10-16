using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Task_The_Kings_Dawid_Ruth
{
    public class Monarchs
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nm")]
        public string Name { get; set; }

        [JsonProperty("cty")]
        public string City { get; set; }

        [JsonProperty("hse")]
        public string House { get; set; }

        [JsonProperty("yrs")]
        public string Years { get; set; }

        public int ReignStart { get; set; }
        public int ReignEnd { get; set; }
            
        public void AssignReignYears()  //convert data to years 
        {
            if (!string.IsNullOrEmpty(Years))
            {
                //split string into two years 
                var years = Years.Split('-');
                //assign the first year to the starting year
                if (int.TryParse(years[0], out int startYear))
                {
                    ReignStart = startYear;
                }
                //assign the second year to the end year, if it exists
                if (years.Length > 1 && int.TryParse(years[1], out int endYear))
                {
                    ReignEnd = endYear;
                }
                else if (years.Length==1) //some rulers ruled for only one year
                {
                    ReignEnd = ReignStart;
                }
                //case for the current ruler 
                else if (years.Length>1)
                {
                    ReignEnd = DateTime.Now.Year;
                }
            }
        }

        public int CountReignYears()
        {
            int yearCount = 0;
            //both dates have to be bigger than 0 and end year > start year
            if (ReignStart >= 0 && ReignEnd >= 0 && ReignEnd > ReignStart)
            {
                yearCount = ReignEnd - ReignStart;
                //return yearCount;

            }
            //for years before year 0 but its no use in our case
            //if (ReignStart < 0) 
            //{
            //    count = (ReignStart - ReignEnd)*(-1); 
            //}
            return yearCount;
        }


    }

}
