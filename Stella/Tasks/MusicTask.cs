using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using Stella.Tasks.Interfaces;
using System;

namespace Stella.Tasks
{
    internal class MusicTask : IRecognitionTask
    {
        private readonly string _command;
        private IWebDriver _driver;
        public MusicTask(string command) {
            _command = command;
        }
        public void Run()
        {
            _driver = new EdgeDriver();

            string url = $"https://music.youtube.com/search?q={_command.Replace(" ","+")}";
            _driver.Navigate().GoToUrl(url);


            WebDriverWait _wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 7));
            IWebElement link;

            try
            {
                link = _wait.Until(d => d.FindElement(By.XPath("//*[@id=\"contents\"]/ytmusic-card-shelf-renderer/div/div[2]/div[1]/div/div[2]/div[1]/yt-formatted-string/a")));
                link.Click();
            }
            catch
            {
                Console.WriteLine("Waiting for page loading");
            }

            try
            {
                link = _wait.Until(ElementIsClickable(By.XPath("//*[@id=\"skip-button:5\"]/span/button")));

                link.Click();
            }
            catch
            {
                Console.WriteLine("Waiting for page loading");
            }
        }
        public void Stop()
        {
            _driver.Quit();
        }

        private Func<IWebDriver, IWebElement> ElementIsClickable(By locator)
        {
            return driver =>
            {
                var element = driver.FindElement(locator);
                return (element != null && element.Displayed && element.Enabled) ? element : null;
            };
        }
    }
}
