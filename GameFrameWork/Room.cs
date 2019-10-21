using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Room : Scene
    {
        private Room _north;
        private Room _south;
        private Room _east;
        private Room _west;

        public Room(): this(12, 6)
        {

        }

        public Room(int sizeX, int sizeY) : base(sizeX, sizeY)
        {

        }

        public Room North
        {
            get
            {
                return _north;
            }
            set
            {
                ////if we are setting North to another room
                //if (value != null)
                //{
                //    value._south = this;
                //}
                ////if we are clearing 
                //else
                //{
                //    _north._south = null;
                //}
                _north = value;
            }
        }
        public Room South
        {
            get
            {
                return _south;
            }
            set
            {
                _south = value;
                //value._north = this;
            }
        }
        public Room East
        {
            get
            {
                return _east;
            }
            set
            {
                _east = value;
                //value._west = this;
            }
        }
        public Room West
        {
            get
            {
                return _west;
            }
            set
            {
                _west = value;
                //value._east = this;
            }
        }
    }
}
