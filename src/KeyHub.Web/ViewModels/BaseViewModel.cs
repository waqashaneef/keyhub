﻿using KeyHub.Data;
using KeyHub.Web.ViewModels.User;
using System.Web;

namespace KeyHub.Web.ViewModels
{
    /// <summary>
    /// Generic converter to create a bridge between domain model and view model
    /// </summary>
    /// <typeparam name="TEntity">Type of the datamodel entity represented by this viewmodel</typeparam>
    /// <remarks>
    /// Create a viewmodel that PERFECTLY fits a specific view, including data it needs in selectionlists etc. 
    /// In this way the Controller is responsible for data retrieval in form of entities,
    /// the ViewModel is responsible for:
    /// - data formatting to and from an entity
    /// - metadata such as error messages, and displaynames
    /// - conversion from list to selectlist
    /// - etc
    /// for View ONLY presents the viewmodel
    /// </remarks>
    public abstract class BaseViewModel<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Parameterless Constructor is public so ViewModel can be a parameter in the Controller Method postback
        /// </summary>
        public BaseViewModel()
        { }

        public string RedirectUrl { get; set; }
    }
}