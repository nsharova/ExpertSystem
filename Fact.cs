using System;
namespace ExpertSystem
{
    public class Fact
    {
        public bool? value { get; set; }
        public char name { get; set; }
        public bool negative { get; set; }

        public Fact(bool? value, char name, bool negative)
        {
            this.value = value;
            this.name = name;
            this.negative = negative;
        }

        public Fact(bool? value, char name)
        {
            this.value = value;
            this.name = name;
         
        }

        public Fact(bool? value)
        {
            this.value = value;
            this.name = name;

        }
    
    }
}
