﻿using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Product : IRecord
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public int Name { get; set; }

        public int[] KeyCode { get; set; } 

        /// <summary>
        /// Optional value for products in packs. <br>
        /// If null is "free"
        /// </summary>
        public double? PortionWeight { get; set; }
    }
}
