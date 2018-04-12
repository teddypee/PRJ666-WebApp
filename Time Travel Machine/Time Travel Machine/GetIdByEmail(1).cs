        [Authorize]
        public int GetIdByEmail(string email)
        {

            int userID = 0;


            using (MySqlConnection connection = new MySqlConnection(mysqlconnection))
            {
                try
                {

                    MySqlCommand getID = new MySqlCommand();


                    // connect to database
                    connection.Open();

                    // check if user is logged in and currently in session

                        getID = new MySqlCommand("SELECT USER_ID FROM USER WHERE EMAIL = @Email", connection);
                        getID.Parameters.AddWithValue("@Email", email.ToString());



                        userID = (int)getID.ExecuteScalar();

                        return userID;


                }

                catch (Exception ex)
                {

                }
                finally
                {
                    // close the connection
                    connection.Close();

                }

            }

            return userID;

        }


        [Authorize]
        public int GetIdByEmail()
        {

            int userID = 0;


            using (MySqlConnection connection = new MySqlConnection(mysqlconnection))
            {
                try
                {

                    MySqlCommand getID = new MySqlCommand();


                    // connect to database
                    connection.Open();

                    // check if user is logged in and currently in session

                    if(Session["CurrentUser"] != null)
                    { 

                        getID = new MySqlCommand("SELECT USER_ID FROM USER WHERE EMAIL = @Email", connection);
                        getID.Parameters.AddWithValue("@Email", Session["CurrentUser"].ToString());



                        userID = (int)getID.ExecuteScalar();

                        return userID;


                    }

                    else
                    {
                        return -1;
                    }

                }

                catch (Exception ex)
                {

                }
                finally
                {
                    // close the connection
                    connection.Close();

                }

            }

            return userID;


        }
