**Video File Management API**

This is a .NET Core Web API application that manages video files associated with products. The application allows users to upload, view, and delete video files for each product. The backend is developed using .NET Core, and the data is stored and managed in a MySQL database. Redis is used as a distributed cache to improve performance.

**Prerequisites**

i) .NET Core SDK (version 6.0 or later)

ii) MySQL Server

ii) Redis Server

**Setup**

**1.Clone the repository:**
```git clone https://github.com/your-username/video-file-management-api.git```

**2.Navigate to the project directory:**
```cd video-file-management-api```

**3.Create a new MySQL database and update the connection string in the appsettings.json file:**

<pre>"ConnectionStrings": {
  "DefaultConnection": "server=YOUR_MYSQL_SERVER;database=YOUR_DATABASE_NAME;user=YOUR_USERNAME;password=YOUR_PASSWORD"
}</pre>

**4.Update the Redis connection string in the appsettings.json file if necessary:**
<pre>
"ConnectionStrings": {
  "Redis": "localhost:6379"
}</pre>

**5.Run the application:**
<pre>Copy codedotnet run</pre>


The application will start running at ``http://localhost:5000.``

**Usage**

You can interact with the API using tools like Postman, Insomnia, or any other HTTP client. Refer to the API Documentation for detailed information about available endpoints, request/response formats, and parameters.


**Technologies Used**

i) .NET Core

ii) ASP.NET Core Web API

iii) Entity Framework Core

iv) MySQL

v) Redis

**Contributing**

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

**License**

This project is licensed under the MIT License.
Feel free to customize and expand the README file based on your project's specific requirements, such as adding sections for testing, deployment, or additional features.

