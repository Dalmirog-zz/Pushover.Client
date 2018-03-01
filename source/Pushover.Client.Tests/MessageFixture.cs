using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Pushover.Client;
using Pushover.Client.Exceptions;

namespace Pushover.Client.Tests
{
    [TestFixture]
    public class MessageFixture
    {
        [Test]
        public void TitleCannotBeLongerThanMaxAllowedValue()
        {
            Assert.Throws<PropertyLengthTooLongException>(
                delegate { new PushoverMessage(title: new string('*', PropertyMaxLength.Title + 1));});
        }

        [Test]
        public void MessageCannotBelongerThanMaxAllowedValue()
        {
            Assert.Throws<PropertyLengthTooLongException>(
                delegate { new PushoverMessage(message: new string('*', PropertyMaxLength.Message + 1)); });
        }

        [Test]
        public void UrlCannotBelongerThanMaxAllowedValue()
        {
            Assert.Throws<PropertyLengthTooLongException>(
                delegate { new PushoverMessage(url: new string('*', PropertyMaxLength.URL +1 )); });
        }

        [Test]
        public void URL_TitleCannotBelongerThanMaxAllowedValue()
        {
            Assert.Throws<PropertyLengthTooLongException>(
                delegate { new PushoverMessage(url_title: new string('*', PropertyMaxLength.URL_Title +1 )); });
        }
    }
}
