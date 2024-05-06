Public Class Form1

    Private costofBicycle, costofFootball, costofVball, costofIBoat As Double
    Private totalAmount, discountAmount, TaxableAmount, taxAmount, payableAmount As Double

    Private receiptDetails As String = "{0,-20}{1,-25}{2,-15}{3,-20}{4,-15}"

    Private Sub btnSet_Click(sender As Object, e As EventArgs) Handles btnSet.Click


        'convert user input and assign to variables
        Double.TryParse(txtBicycle.Text, costofBicycle)
        'costofFootball = CDbl(txtFBall.Text)
        Double.TryParse(txtFBall.Text, costofFootball)
        Double.TryParse(txtVBall.Text, costofVball)
        Double.TryParse(txtIBoat.Text, costofIBoat)

        MessageBox.Show("Program Setup completed successfully.")
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim itemName As String
        Static itemQuantity, numOfDays As Integer
        Dim itemRentCost, totalItemRentCost As Double
        Dim costDiscount, membershipDiscount As Double
        itemName = cmbItemName.SelectedItem

        itemQuantity = nduQuantity.Value
        numOfDays = nduNumOfDays.Value

        'check what the user selected from the combbox to match the price
        If itemName = "Bicycle" Then
            itemRentCost = costofBicycle
        ElseIf itemName = "Football" Then
            itemRentCost = costofFootball
        ElseIf itemName = "Volley Ball" Then
            itemRentCost = costofVball
        Else
            itemRentCost = costofIBoat
        End If

        totalItemRentCost = itemRentCost * itemQuantity * numOfDays

        totalAmount += totalItemRentCost


        'check member status and give discount 
        Select Case totalAmount
            Case Is < 200
                costDiscount = 0.02 * totalAmount
            Case Is <= 1000
                costDiscount = 0.05 * totalAmount
            Case Else
                costDiscount = 0.1 * totalAmount

        End Select

        If rbtMember.Checked = True Then
            membershipDiscount = 0.05 * totalAmount
        End If
        discountAmount = costDiscount + membershipDiscount

        TaxableAmount = totalAmount - discountAmount

        taxAmount = 0.12 * TaxableAmount

        payableAmount = TaxableAmount + taxAmount

        lstReceipt.Items.Add(String.Format(receiptDetails, itemName, itemQuantity.ToString, numOfDays.ToString, itemRentCost.ToString("C2"), totalItemRentCost.ToString("C2")))
        txtTotal.Text = totalAmount.ToString("C2")
        txtDiscount.Text = FormatCurrency(discountAmount, 3)
        txtTaxableAmount.Text = TaxableAmount.ToString("C2")
        txtVat.Text = taxAmount.ToString("C2")
        txtPayableAmount.Text = payableAmount.ToString("C2")


    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lstReceipt.Items.Add(String.Format(receiptDetails, "ITEM", "QUANTITY", "NUM OF DAYS", "UNIT COST", "TOTAL COST"))

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim mydialogResult As DialogResult
        mydialogResult = MessageBox.Show("Do you really want to Exit?", "Exit ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If mydialogResult = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lstReceipt.Items.Clear()
        lstReceipt.Items.Add(String.Format(receiptDetails, "ITEM", "QUANTITY", "NUM OF DAYS", "UNIT COST", "TOTAL COST"))
        txtTotal.Clear()
        txtDiscount.Clear()
        txtPayableAmount.Clear()
        txtTaxableAmount.Clear()
        txtVat.Clear()
    End Sub
    Private Sub btnRemoveItem_Click(sender As Object, e As EventArgs) Handles btnRemoveItem.Click
        receiptDetails = lstReceipt.SelectedItem
        Dim newTotalAmount As Double = CDbl(receiptDetails.Remove(0, 80))
        lstReceipt.Items.RemoveAt(lstReceipt.SelectedIndex)
        totalAmount -= newTotalAmount

        txtTotal.Text = totalAmount.ToString("C2")
        txtDiscount.Text = FormatCurrency(discountAmount, 3)
        txtTaxableAmount.Text = TaxableAmount.ToString("C2")
        txtVat.Text = taxAmount.ToString("C2")
        txtPayableAmount.Text = payableAmount.ToString("C2")


    End Sub
    Private Sub lstReceipt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstReceipt.SelectedIndexChanged
        'receiptDetails = lstReceipt.SelectedItem
        receiptDetails += 1
        Label13.Text = receiptDetails.Remove(0, 84)


    End Sub
End Class
