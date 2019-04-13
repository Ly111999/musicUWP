﻿using AgainUWP.Emtity;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgainUWP.Data
{
    class DataHandle
    {
        public static void AddRecentSong(Song currentSong)
        {
            using (SqliteConnection db =
        new SqliteConnection("Filename=Song.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                string time = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.ffz");
                if (currentSong.description == null)
                {
                    currentSong.description = "Unknown";
                }
                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT OR REPLACE INTO Recent VALUES " +
                    "( COALESCE((SELECT id FROM Recent WHERE id = @id), @id), @name, @description, @singer, @author, @thumbnail, @link, @createdAt, @updatedAt);";
                insertCommand.Parameters.AddWithValue("@createdAt", currentSong.createdAt);
                insertCommand.Parameters.AddWithValue("@updatedAt", time);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }
    }
}