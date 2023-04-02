using Database;
using PleasantRustle.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WSUniversalLib;

namespace PleasantRustle
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PleasantRustleEntities _db;
        public MainWindow()
        {
            InitializeComponent();

            _db = new PleasantRustleEntities();

            var agentType = PleasantRustleEntities.GetContext().AgentType.Select(w=>w.typeName).ToList();
            agentType.Insert(0, "Все типы");

            filterComboBox.ItemsSource = agentType;
            filterComboBox.SelectedIndex= 0;

            string[] allSortVariable = new string[] { "Название по возрастанию", "Название по убыванию", /*"Размер скидки по возрастанию", "Размер скидки по убыванию",*/ "Приоритет по возрастанию", "Приотритет по убыванию"};

            sortComboBox.ItemsSource = allSortVariable;
            sortComboBox.SelectedIndex = 0;

            AgentsUpdate();
        }

        public void AgentsUpdate()
        {
            agentListView.ItemsSource = null;
            var currentAgents = _db.Agent.ToList();
            int discount = 0;
            List<AgentDto> agents = new List<AgentDto>();
            var calculation = new Calculation();
            var nowYear = new DateTime();
            nowYear = Convert.ToDateTime("2018-01-01");
            Nullable<int> amountSales = 0;
            foreach (var agent in currentAgents)
            {
           
                amountSales = _db.Sales.Where(x => x.agentId == agent.id && x.realizeDate >= nowYear).Sum(x => x.count);
                discount = calculation.GetDiscountForAgent(_db, agent); 
                agents.Add(new AgentDto {
                    name = agent.name,
                    logoPath = agent.logoPath,
                    priority = agent.Priority,
                    phoneNumber = agent.phoneNumber,
                    typeAgent = _db.AgentType.FirstOrDefault(x => x.id == agent.typeAgent).typeName,
                    discount = discount,
                    saleOfYearCount = amountSales ?? 0,
                    email = agent.email
                });
            }
            if (filterComboBox.SelectedIndex > 0)
            {
                string filter = filterComboBox.SelectedItem.ToString();
                var allTypeAgents = _db.AgentType.ToList();
                agents = agents.Where(t=> t.typeAgent == filter).ToList();
                
            }
            

            switch (sortComboBox.SelectedItem.ToString())
            {
                case "Название по возрастанию":
                    agents = agents.OrderBy(p => p.name).ToList();
                    break;

                case "Название по убыванию":
                    agents = agents.OrderByDescending(p => p.name).ToList();
                    break;
                
                case "Приоритет по возрастанию":
                    agents = agents.OrderBy(p => p.priority).ToList();
                    break;

                case "Приотритет по убыванию":
                    agents = agents.OrderByDescending(p => p.priority).ToList();
                    break;

                case "Размер скидки по возрастанию":
                    agents = agents.OrderBy(p => p.discount).ToList();
                    break;

                case "Размер скидки по убыванию":
                    agents = agents.OrderByDescending(p => p.discount).ToList();
                    break;
            }

            if (searchTextBox.Text != "")
            {
                agents = agents
                    .Where(p => p.name.ToLower()
                    .Contains(searchTextBox.Text.ToLower()) 

                    || p.phoneNumber.ToLower()
                    .Contains(searchTextBox.Text.ToLower()) 

                    || p.email.ToLower()
                    .Contains(searchTextBox.Text.ToLower()))
                    .ToList();
            }

            agentListView.ItemsSource = agents;

        }


        #region ContextMenu кнопки
        private void addAgentMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editAgentMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeAgentMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
