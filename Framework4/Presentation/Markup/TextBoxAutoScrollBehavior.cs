//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

//using System;
//using Xamarin.Forms;

//namespace Tiny.Framework.Markup
//{
//    public sealed class TextBoxAutoScrollBehavior : Behavior<Editor>
//    {
//        /// <summary>
//        /// Gets or sets the associated object.
//        /// </summary>
//        private Editor AssociatedObject { get; set; }

//        protected override void OnAttachedTo(Editor bindable)
//        {
//            base.OnAttachedTo(bindable);
//            AssociatedObject = bindable;
//            AssociatedObject.TextChanged += TextChanged;
//        }

//        protected override void OnDetachingFrom(Editor bindable)
//        {
//            base.OnDetachingFrom(bindable);

//            AssociatedObject.TextChanged -= TextChanged;
//        }

//        private void TextChanged(object sender, TextChangedEventArgs e)
//        {
//            AssociatedObject..ScrollToEnd();
//        }
//    }
//}