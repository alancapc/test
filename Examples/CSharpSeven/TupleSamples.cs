﻿namespace Examples.CSharpSeven
{
    public class TupleSamples
    {
        public (string name, string title, long year) GetNewTuple()
        {
            return (name: "Alan Costa", title: ".NET Programming", year: 2017);
        }
    }
}