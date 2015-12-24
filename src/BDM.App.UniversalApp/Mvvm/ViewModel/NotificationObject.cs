using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BDM.App.UniversalApp.Mvvm.ViewModel
{
    public class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            if (propertyExpression != null)
            {
                var memberExpression = propertyExpression.Body as MemberExpression;
                if (memberExpression != null)
                {
                    var property = memberExpression.Member as PropertyInfo;
                    if (property != null)
                    {
                        var getMethod = property.GetMethod;
                        if (!getMethod.IsStatic)
                        {
                            RaisePropertyChangedImpl(property.Name);
                        }
                        else
                        {
                            throw new ArgumentException("The referenced property is a static property.", "propertyExpression");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");
                    }
                }
                else
                {
                    throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");
                }
            }
            else
            {
                throw new ArgumentNullException("propertyExpression");
            }
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            RaisePropertyChangedImpl(propertyName);
        }

        private void RaisePropertyChangedImpl(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
