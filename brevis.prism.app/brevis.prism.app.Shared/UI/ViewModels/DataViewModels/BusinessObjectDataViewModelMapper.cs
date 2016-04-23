using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Prism.StoreApps.Interfaces;

namespace brevis.prism.app.UI.ViewModels.DataViewModels
{
    public class BusinessObjectDataViewModelMapper
    {
        private IResourceLoader _resourceLoader;

        public BusinessObjectDataViewModelMapper(IResourceLoader resourceLoader)
        {
            if (resourceLoader == null) throw new ArgumentNullException("resourceLoader");
            _resourceLoader = resourceLoader;
        }
    }
}
