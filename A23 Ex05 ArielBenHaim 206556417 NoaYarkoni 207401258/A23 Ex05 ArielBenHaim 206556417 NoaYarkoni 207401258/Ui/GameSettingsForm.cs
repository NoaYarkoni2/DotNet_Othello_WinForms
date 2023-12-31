using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Ui
{
    public partial class GameSettingsForm : Form
    {
        
        private int m_BoardSize=6;
        private int m_GameMode;
        private GameForm m_GameForm;
        public GameSettingsForm()
        {
            InitializeComponent();          
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
            set
            {
               m_BoardSize = value;
            }
        }
        private void GameSettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void GameSettingsForm_Load_1(object sender, EventArgs e)
        {

        }
        private void GameSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void buttonOnePlayer_Click (object sender, EventArgs e)
        {
            m_GameMode = 1;
            BuildGameFromSettings();
            this.Close();
            m_GameForm.ShowDialog();
        }

        private void buttonTwoPlayers_Click (object sender, EventArgs e)
        {          
            m_GameMode = 2;
            BuildGameFromSettings();
            this.Close();
            m_GameForm.ShowDialog();       
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            this.m_BoardSize = (this.m_BoardSize + 2) % 14;
            if (m_BoardSize == 0)
            {
                m_BoardSize = 6;
            }

            string newText = string.Format("Board Size: {0}x{1} (click to increase)", this.m_BoardSize, this.m_BoardSize);
            buttonBoardSize.Text = newText.ToString();
        }

        private void BuildGameFromSettings()
        {                     
            Game game = new Game(m_BoardSize);
            game.InitGame(m_BoardSize, m_GameMode);
            m_GameForm = new GameForm(m_BoardSize, m_GameMode, game);
        }
    }
}
