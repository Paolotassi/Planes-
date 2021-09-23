Imports System.Threading

Public Class MainMenu



    Dim nf As New NewFlight
    Dim fpe As New FlightPlanEditor



    Private Sub Menu_Iniziale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Menu_Iniziale_Close(sender As Object, e As EventArgs) Handles MyBase.Closed

    End Sub

    'Se viene premuto "Flight Plan Editor" svolgi le procedure della relativa finestra
    Private Sub ButtonCreateFlightPlan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFlightPlanEditor.Click

        'Apri la finestra

        fpe.TopMost = True
        fpe.Show()

    End Sub

    'Se viene premuto "New Flight" svolgi le procedure della relativa finestra
    Private Sub ButtonNewFlight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNewFlight.Click

        'Apri la finestra

        nf.TopMost = True
        nf.Show()

    End Sub

    'Se viene premuto "Review Flight" svolgi le procedure della relativa finestra
    Private Sub ButtonReviewFlight_Click(sender As Object, e As EventArgs) Handles ButtonReviewFlight.Click

    End Sub

End Class
