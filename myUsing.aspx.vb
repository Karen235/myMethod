
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration


Partial Class myUsing
    Inherits System.Web.UI.Page

    Private Sub myUsing_Load(sender As Object, e As EventArgs) Handles Me.Load



    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ConnStatus()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Write(ReadFile)
    End Sub

    Private Function ConnStatus() As String

        Dim CnSection As ConnectionStringsSection = WebConfigurationManager.GetWebApplicationSection("connectionStrings")
        Dim strconn As String = CnSection.ConnectionStrings("conn").ConnectionString

        Using cn As New SqlConnection(strconn)

            Dim cmd As New SqlCommand
            cmd.Connection = cn
            cmd.CommandText = "select * from person where wid=@wid"
            cmd.Parameters.AddWithValue("@wid", "010100")


            Try

                cn.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                Do While dr.Read()
                    'ConnStatus =
                    '  String.Format(
                    '  vbTab & "{0}" & vbTab & "{1}" & vbTab & "{2}",
                    ' dr(0), dr(1), dr(2)
                    ' )


                    ConnStatus = String.Format(
                        vbTab & "{0}",
                     dr(0)
                    )
                Loop



            Catch ex As Exception

            Finally
                ConnStatus = cn.State.ToString
            End Try
        End Using


    End Function


    Private Function ReadFile() As String

        ReadFile = ""

        '使用 using 陳述式時，雖然有 try 與 finally，但並不包含 catch 的成分！
        '使用 using 陳述式並不會幫你捕捉例外狀況！
        Dim path As String = Server.MapPath("test.txt")

        Using sr As New System.IO.StreamReader(path)

            Try
                Dim s As String = Nothing
                While sr.Peek > 0
                    s = sr.ReadLine
                    ReadFile += s
                End While
            Finally

                If Not sr Is Nothing Then
                    sr.Dispose()
                End If

            End Try

        End Using




    End Function




End Class
