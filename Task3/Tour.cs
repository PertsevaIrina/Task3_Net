using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Task3
{
    public class Tour
    {
        private TourType _type;
        private List<Transport> _transports;
        private Int16 _meals;
        private Int16 _days;

        public Tour(TourType type, List<Transport> transports, short meals, short days)
        {
            _type = type;
            _transports = transports;
            _meals = meals;
            _days = days;
        }

        public TourType Type => _type;

        public List<Transport> Transports => _transports;

        public short Meals => _meals;

        public short Days => _days;
    }
}