using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using tetris.Add_classes;

namespace tetris
{
    public class MyAnnotationAttribute:ValidationAttribute
    {
            public override bool IsValid(object? value)
            {
            DataBase database = new DataBase();
            if (value is string WH)
            {
                string queryString = $"SELECT Glass_Id FROM Glass WHERE Width = {Convert.ToInt32(WH.Substring(0,2))} AND Length = {Convert.ToInt32(WH.Substring(2, 2))} AND Glass_Id != {Convert.ToInt32(WH.Substring(4))}";
                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                database.openConnection();
                SqlDataReader reader5 = command.ExecuteReader();
                if (!reader5.Read())
                {
                    reader5.Close();
                    return true;
                }
                else
                {
                    reader5.Close();
                    ErrorMessage = "Такой стакан уже существует!";
                }
            }
            database.closeConnection();
            return false;
            }
    }
}
