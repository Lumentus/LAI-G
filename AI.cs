using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Text.RegularExpressions;

namespace SLSGAI
{
    enum State
    {
        greeting = 1,
        askingForUserName = 2,
        askingForOwnName = 3
    }

    class AI
    {
        private static String unnamedAIName = "Unnamed AI";
        private String name;
        private int language;
        private String gameName;
        private mainForm form;
        private SqlCeConnection dbConn;
        private bool greeted;
        private State state;
        private Regex dbEx;

        public String Name
        {
            get { return name; }
        }
        /**
         * In this constructor the AI saves the form in a member, so it can communicate with the form later
         * As well it builds up the connection to the database/the brain and gets all settings for
         * the AI in members
         **/
        public AI(mainForm form)
        {
            this.form = form;
            // Create Database connection, or as the AI says if it fails connect to its brain
            dbConn = new SqlCeConnection("Data Source=\"..\\..\\brain.sdf\";");
            this.name = (String)this.getPropertie("name");
            this.language = Convert.ToInt32((String)this.getPropertie("standardLanguage"));
            dbEx = new Regex(@"^%(?<table>\w+)_(?<id>\w+)$", RegexOptions.IgnoreCase);
            // Start Zustand definieren
            showMessage(upperCaseFirstLetter(getPhrase("greeting")));
        }

        /**
         * Analyses a given message
         * the return value does tell the main program, whether the AI wants to start a game or not(if it should call startGame method
         **/
        public void analyseMessage(String msg)
        {
            msg.ToLower();
            String meaning = getMeaning(msg);
            // Here the AI should analyse the message and take its actions
            if (meaning == "test" || messageAppliesPattern(msg, "%m_execute_smt test"))
            {
                test();
            }
            else
            {
                if (meaning != null)
                {
                    analyseMeaning(meaning);
                }
                else
                {
                    String[] sentences = msg.Split((new char[] { '?', '!', '.' }));
                    String[] outputs = new String[sentences.Length];
                    for (int i = 0; i < sentences.Length; i++)
                    {
                        outputs[i] = analyseSentence(sentences[i]);
                        showMessage(outputs[i]);
                    }
                }
            }
        }

        private String analyseSentence(String sentence)
        {
            String answer = "";
            String[] parts = sentence.Trim().Split(' ');
            return answer;
        }

        private void analyseMeaning(String meaning)
        {
        }

        private void analyseMeaning(String[] meaning)
        {
        }

        private void startGame()
        {

        }

        private void stopGame()
        {
        }

        private void playGame()
        {
        }

        private Object getPropertie(String setting)
        {
            Object ret = null;
            SqlCeDataReader dataReader = null;
            openDBConnection();
            try
            {
                // Create the SQLCommand we want to execute
                SqlCeCommand cmd = new SqlCeCommand("SELECT p.value FROM properties AS p WHERE p.setting='" + setting + "'", dbConn);
                // Run the command and put the result into the reader
                dataReader = cmd.ExecuteReader();
                // Run over ALL the data and save the requested propertie in ret
                while (dataReader.Read())
                {
                    if (dataReader[0] is DBNull)
                    {
                        ret = null;
                    }
                    else
                    {
                        ret = dataReader[0];
                    }
                }
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                closeDBConnection();
            }
            return ret;
        }

        private void updatePropertie(String name, String value)
        {
            openDBConnection();
            try
            {
                // Create the SQLCommand we want to execute
                SqlCeCommand cmd = new SqlCeCommand("UPDATE properties SET value='" + value + "' WHERE setting='" + name + "'", dbConn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                closeDBConnection();
            }
        }

        private void openDBConnection()
        {
            try
            {
                // Open the connection to the database
                dbConn.Open();
            }
            catch
            {
                showMessage("I can't find my brain. Have to shutdown.", "Confused AI");
                System.Threading.Thread.Sleep(5000);
                form.shutdown();
            }
        }

        private void closeDBConnection()
        {
            dbConn.Close();
        }

        private void showMessage(String msg)
        {
            if (this.name == null)
            {
                form.showMessage(msg, AI.unnamedAIName);
            }
            else
            {
                form.showMessage(msg, this.name);
            }
        }

        private void showMessage(String msg, String name)
        {
            form.showMessage(msg, name);
        }

        private String getPhrase(String meaning)
        {
            String ret = null;
            SqlCeDataReader dataReader = null;
            openDBConnection();
            try
            {
                String sql =    @"SELECT p.text 
                                FROM phrase AS p 
                                INNER JOIN meaning as m 
                                ON m.id = p.meaning 
                                WHERE m.meaning='" + meaning + @"'
                                AND p.language=" + this.language;
                // Create the SQLCommand we want to execute
                SqlCeCommand cmd = new SqlCeCommand(sql, dbConn);
                // Run the command and put the result into the reader
                dataReader = cmd.ExecuteReader();
                // Run over ALL the data and save the requested propertie in ret
                while (dataReader.Read())
                {
                    if (!(dataReader[0] is DBNull))
                    {
                        ret = (String)dataReader[0];
                        break;
                    }
                }
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                closeDBConnection();
            }
            return ret;
        }

        private String getMeaning(String phrase)
        {
            String ret = null;
            SqlCeDataReader dataReader = null;
            openDBConnection();
            try
            {
                String sql = @"SELECT m.meaning
                                FROM phrase AS p 
                                INNER JOIN meaning as m 
                                ON m.id = p.meaning 
                                WHERE p.text='" + phrase + @"'
                                AND p.language=" + this.language;
                // Create the SQLCommand we want to execute
                SqlCeCommand cmd = new SqlCeCommand(sql, dbConn);
                // Run the command and put the result into the reader
                dataReader = cmd.ExecuteReader();
                // Run over ALL the data and save the requested propertie in ret
                while (dataReader.Read())
                {
                    if (!(dataReader[0] is DBNull))
                    {
                        ret = (String)dataReader[0];
                        break;
                    }
                }
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                closeDBConnection();
            }
            return ret;
        }

        private List<String> getPhrases(String meaning)
        {
            List<String> ret = new List<String>();
            SqlCeDataReader dataReader = null;
            openDBConnection();
            try
            {
                String sql = @"SELECT p.text 
                                FROM phrase AS p 
                                INNER JOIN meaning as m 
                                ON m.id = p.meaning 
                                WHERE m.meaning='" + meaning + @"'
                                AND p.language=" + this.language;
                // Create the SQLCommand we want to execute
                SqlCeCommand cmd = new SqlCeCommand(sql, dbConn);
                // Run the command and put the result into the reader
                dataReader = cmd.ExecuteReader();
                // Run over ALL the data and save the requested propertie in ret
                while (dataReader.Read())
                {
                    if (!(dataReader[0] is DBNull))
                    {
                        ret.Add((String)dataReader[0]);
                    }
                }
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                closeDBConnection();
            }
            return ret;
        }

        private Boolean messageAppliesPattern(String msg, String pattern)
        {
            String table, argument;
            String[] partsMsg = msg.Split(' ');
            String[] partsPattern = pattern.Split(' ');
            if (partsMsg.Length != partsPattern.Length)
            {
                return false;
            }
            for (int i = 0; partsPattern.Length > i; i++)
            {
                Match match = dbEx.Match(partsPattern[i]);
                if (match != null)
                {
                    table = match.Groups["table"].Value;
                    argument = match.Groups["id"].Value;
                    switch (table)
                    {
                        case "m":
                            List<String> temp = getPhrases(argument);
                            if (!temp.Contains(partsMsg[i]))
                            {
                                return false;
                            }
                            break;
                    }
                }
                else
                {
                    if (partsPattern[i] != partsMsg[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public String upperCaseFirstLetter(String text)
        {
            Char[] charText = text.ToCharArray();
            charText[0] = char.ToUpper(charText[0]);
            return new String(charText);
        }
        
        private void test()
        {
            showMessage("Test erreicht!");
        }
    }
}
