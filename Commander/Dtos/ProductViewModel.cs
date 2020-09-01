
//-----------------------------------------------------------------------  
// <copyright file="ProductViewModel.cs" company="None">  
//     Copyright (c) Allow to distribute this code and utilize this code for personal or commercial purpose.  
// </copyright>  
// <author>Asma Khalid</author>  
//-----------------------------------------------------------------------  

namespace Commander.Dtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Commander.Models.DB;

    /// <summary>  
    /// Product view model class.  
    /// </summary>  
    public class ProductViewModel
    {
        #region Properties  

        /// <summary>  
        /// Gets or sets product ID property.  
        /// </summary>  
        [Required]
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }

        /// <summary>  
        /// Gets or sets to products list whose price is greater than equal to 1000 property.  
        /// </summary>  
        [Display(Name = "Products with Price >= 1000")]
        public List<SpGetProductByPriceGreaterThan1000> ProductsGreaterThan1000 { get; set; }

        /// <summary>  
        /// Gets or sets to product detail by product Id property.  
        /// </summary>  
        [Display(Name = "Product Detail")]
        public SpGetProductByID ProductDetail { get; set; }

        #endregion
    }
}