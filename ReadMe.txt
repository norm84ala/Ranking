Please create a DB called "Webs" in MSsqlserver
Use Ranking\DBBACKUP\webs.bak to update Webs db
updete the connection string accrodingly to connect app to thenewly restored DB

   <add name ="websDB" connectionString="server=[servername]; database= webs; integrated security = SSPI"/>