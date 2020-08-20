using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class DataBaseManager : MonoBehaviour
{
    public static string[] valuesForEnemy;
    public static string[] valuesGameSave;
    public static string[] valuesForPerson;
    public static string[] valuesForAllObjects;
    public static string[] valuesForPersonInventory;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start DataBaseManager");
        SqliteConnection dbconn = new SqliteConnection("Data Source=" + Application.dataPath + "/Databases/NEWDATABASE.db");
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * " + "FROM AccountInformation;";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {

            int id = reader.GetInt32(0);
            string login = reader.GetString(1);
            string pass = reader.GetString(2);
            string email = reader.GetString(3);
            int regidate = reader.GetInt32(4);
            Debug.Log("id = " + id + " login = " + login + " password = " + pass + " email = " + email + " date = " + regidate);

        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    
    public void SaveGame()
    {
        Debug.Log("Start SaveGame DataBaseManager");
        SqliteConnection dbconn = new SqliteConnection("Data Source=" + Application.dataPath + "/Databases/NEWDATABASE.db");
        SqliteCommand cmd = new SqliteCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = $"INSERT INTO Game (ID_game, ID_user, BeginDate, SaveDate) VALUES ({valuesGameSave[0]},{valuesGameSave[1]},{valuesGameSave[2]},{valuesGameSave[3]})";
        cmd.Connection = dbconn;
        dbconn.Open();
        cmd.ExecuteNonQuery();
        dbconn.Close();
    }
    public void SavePerson()
    {
        Debug.Log("Start SavePerson DataBaseManager");
        SqliteConnection dbconn = new SqliteConnection("Data Source=" + Application.dataPath + "/Databases/NEWDATABASE.db");
        SqliteCommand cmd = new SqliteCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = $"INSERT INTO Game (ID_person, ID_game, Name, Appearence) VALUES ({valuesForPerson[0]},{valuesForPerson[1]},{valuesForPerson[2]},{valuesForPerson[3]})";
        cmd.Connection = dbconn;
        dbconn.Open();
        cmd.ExecuteNonQuery();
        dbconn.Close();
    }

    public string[] LoadPersonInformation()
    {
        string[] info = null;
        SqliteConnection dbconn = new SqliteConnection("Data Source=" + Application.dataPath + "/Databases/NEWDATABASE.db");
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Person;";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {

            string id_person = reader.GetString(0);
            string id_game = reader.GetString(1);
            string name = reader.GetString(2);
            string appear = reader.GetString(3);
            info = new string[]{ id_person, id_game, name, appear };

        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        return info;
    }
    public string[] LoadGameInformation()
    {
        string[] info = null;
        SqliteConnection dbconn = new SqliteConnection("Data Source=" + Application.dataPath + "/Databases/NEWDATABASE.db");
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Game;";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {

            string id_game = reader.GetString(0);
            string id_user = reader.GetString(1);
            string begin_date = reader.GetString(2);
            string save_date = reader.GetString(3);
            info = new string[] { id_game, id_user, begin_date, save_date };

        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        return info;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
