Public Class Main_Menu
    Private Sub Menu_Iniziale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'IsMdiContainer = True
    End Sub

    'Se viene premuto "Flight Plan Editor" svolgi le procedure della relativa finestra
    Private Sub Button_CFP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Create_Flight_Plan.Click

        'Apri la finestra
        Dim cfp As New Create_Flight_Plan
        cfp.TopMost = True
        cfp.Show()

    End Sub



    'Se viene premuto "New Flight" svolgi le procedure della relativa finestra
    Private Sub Button_NF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_New_Flight.Click

        'Apri la finestra
        Dim nf As New Button_Open_Instrument_Panel
        nf.TopMost = True
        nf.Show()

    End Sub

End Class
