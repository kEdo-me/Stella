using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Stella.Tasks.Interfaces;

namespace Stella.Tasks
{
    internal class SearchTask : IRecognitionTask
    {
        private readonly string _command;
        private IWebDriver _driver;
        public SearchTask(string command)
        {
            _command = command;
        }
        public void Run()
        {
            _driver = new EdgeDriver();

            string url = $"https://www.google.com/search?q={_command.Replace(" ", "+")}";
            _driver.Navigate().GoToUrl(url);
        }

        public void Stop()
        {
            _driver.Quit();
        }
    }
}
