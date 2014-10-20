using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class JengaTower
    {
        private readonly List<GameObject> jengaBlocks;

        public JengaTower(List<GameObject> jengaBlocks)
        {
            this.jengaBlocks = jengaBlocks;
        }

        public List<GameObject> GetBlocksOnLevel(int level)
        {
            return jengaBlocks.Where(x =>
                Math.Abs(x.transform.position.y - (level - 0.5f)*x.transform.localScale.y) <
                x.transform.localScale.y*0.5f)
                .ToList();
        } 
    }
}
