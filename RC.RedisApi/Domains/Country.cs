using System;

namespace RC.RedisApi.Domains
{
    public class Country
    {
        public Guid Id { get; private set; }
        public string CountryName { get; set; }


        public Country(string countryName)
        {
            this.Id = Guid.NewGuid();
            this.CountryName = countryName;
        }
    }
}