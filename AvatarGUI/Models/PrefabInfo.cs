using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvatarGUI.Models
{
    public class PrefabInfo: ICloneable
    {
        public string modelName;
        public float position;
        public int rotation;

        public PrefabInfo(string model, float position, int rotation)
        {
            modelName = model;
            this.position = position;
            this.rotation = rotation;
        }

        public object Clone()
        {
            return new PrefabInfo(modelName, position, rotation);
        }
    }
}
