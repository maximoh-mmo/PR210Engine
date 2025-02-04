using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public interface ILayout
    {
        public List<LayoutElement> elements { get; set; }

    }
}
