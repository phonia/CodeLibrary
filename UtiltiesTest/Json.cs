using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.Json;

namespace UtiltiesTest
{
    /// <summary>
    /// Json 的摘要说明
    /// </summary>
    [TestClass]
    public class Json
    {
        public Json()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ToJson()
        {
            Person person = new Person() { Name = "hy", Tel = "15975455335" };
            person.ToJson();
        }

        [TestMethod]
        public void EscapeJavaScriptString()
        {
            string result;

            result = JavaScriptUtils.ToEscapedJavaScriptString("How now brown cow?", '"', true);
            Assert.AreEqual(@"""How now brown cow?""", result);

            result = JavaScriptUtils.ToEscapedJavaScriptString("How now 'brown' cow?", '"', true);
            Assert.AreEqual(@"""How now 'brown' cow?""", result);

            result = JavaScriptUtils.ToEscapedJavaScriptString("How now <brown> cow?", '"', true);
            Assert.AreEqual(@"""How now <brown> cow?""", result);

            result = JavaScriptUtils.ToEscapedJavaScriptString(@"How 
now brown cow?", '"', true);
            Assert.AreEqual(@"""How \r\nnow brown cow?""", result);

            result = JavaScriptUtils.ToEscapedJavaScriptString("\u0000\u0001\u0002\u0003\u0004\u0005\u0006\u0007", '"', true);
            Assert.AreEqual(@"""\u0000\u0001\u0002\u0003\u0004\u0005\u0006\u0007""", result);

            result =
              JavaScriptUtils.ToEscapedJavaScriptString("\b\t\n\u000b\f\r\u000e\u000f\u0010\u0011\u0012\u0013", '"', true);
            Assert.AreEqual(@"""\b\t\n\u000b\f\r\u000e\u000f\u0010\u0011\u0012\u0013""", result);

            result =
              JavaScriptUtils.ToEscapedJavaScriptString(
                "\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f ", '"', true);
            Assert.AreEqual(@"""\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f """, result);

            result =
              JavaScriptUtils.ToEscapedJavaScriptString(
                "!\"#$%&\u0027()*+,-./0123456789:;\u003c=\u003e?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]", '"', true);
            Assert.AreEqual(@"""!\""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]""", result);

            result = JavaScriptUtils.ToEscapedJavaScriptString("^_`abcdefghijklmnopqrstuvwxyz{|}~", '"', true);
            Assert.AreEqual(@"""^_`abcdefghijklmnopqrstuvwxyz{|}~""", result);

            string data =
              "\u0000\u0001\u0002\u0003\u0004\u0005\u0006\u0007\b\t\n\u000b\f\r\u000e\u000f\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f !\"#$%&\u0027()*+,-./0123456789:;\u003c=\u003e?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
            string expected =
              @"""\u0000\u0001\u0002\u0003\u0004\u0005\u0006\u0007\b\t\n\u000b\f\r\u000e\u000f\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f !\""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~""";

            result = JavaScriptUtils.ToEscapedJavaScriptString(data, '"', true);
            Assert.AreEqual(expected, result);

            result = JavaScriptUtils.ToEscapedJavaScriptString("Fred's cat.", '\'', true);
            Assert.AreEqual(result, @"'Fred\'s cat.'");

            result = JavaScriptUtils.ToEscapedJavaScriptString(@"""How are you gentlemen?"" said Cats.", '"', true);
            Assert.AreEqual(result, @"""\""How are you gentlemen?\"" said Cats.""");

            result = JavaScriptUtils.ToEscapedJavaScriptString(@"""How are' you gentlemen?"" said Cats.", '"', true);
            Assert.AreEqual(result, @"""\""How are' you gentlemen?\"" said Cats.""");

            result = JavaScriptUtils.ToEscapedJavaScriptString(@"Fred's ""cat"".", '\'', true);
            Assert.AreEqual(result, @"'Fred\'s ""cat"".'");

            result = JavaScriptUtils.ToEscapedJavaScriptString("\u001farray\u003caddress");
            Assert.AreEqual(result, @"""\u001farray<address""");
        }

        [TestMethod]
        public void EscapeJavaScriptString_UnicodeLinefeeds()
        {
            string result;

            result = JavaScriptUtils.ToEscapedJavaScriptString("before" + '\u0085' + "after", '"', true);
            Assert.AreEqual(@"""before\u0085after""", result);

            result = JavaScriptUtils.ToEscapedJavaScriptString("before" + '\u2028' + "after", '"', true);
            Assert.AreEqual(@"""before\u2028after""", result);

            result = JavaScriptUtils.ToEscapedJavaScriptString("before" + '\u2029' + "after", '"', true);
            Assert.AreEqual(@"""before\u2029after""", result);
        }
    }

    public class Person
    {
        public String Name { get; set; }
        public String Tel { get; set; }
    }
}
