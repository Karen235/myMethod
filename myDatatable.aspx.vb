
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Partial Class myDatatable
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Write(LoadDcatatable())
    End Sub

    Private Function LoadDcatatable() As String

        Dim CnSection As ConnectionStringsSection = WebConfigurationManager.GetWebApplicationSection("connectionStrings")
        Dim strconn As String = CnSection.ConnectionStrings("conn").ConnectionString
        Dim dt As New DataTable

        Using cn As New SqlConnection(strconn)

            Dim cmd As New SqlCommand
            cmd.Connection = cn
            cmd.CommandText = "select * from person where wid=@wid"
            cmd.Parameters.AddWithValue("@wid", "010100")


            Try

                cn.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()

                ''DataTable load DataReader

                dt.Load(dr)



            Finally
                LoadDcatatable = dt.Rows(0).Item("c_name")

            End Try
        End Using


    End Function


End Class
