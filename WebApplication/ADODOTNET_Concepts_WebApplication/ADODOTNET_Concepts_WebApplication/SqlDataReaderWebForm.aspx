<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SqlDataReaderWebForm.aspx.cs" Inherits="ADODOTNET_Concepts_WebApplication.GridViewWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblEmployeeName" Text="Employee Name" runat="server" Width="200"></asp:Label>
            <asp:TextBox ID="txtEmployeeName" runat="server" Width="200"></asp:TextBox>
            <br />
            <asp:Label ID="lblGender" Text="Gender" runat="server" Width="200"></asp:Label>
            <asp:DropDownList ID="ddlGender" runat="server" Width="200">
                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblSalary" Text="Salary" runat="server" Width="200"></asp:Label>
            <asp:TextBox ID="txtSalary" runat="server" Width="200" TextMode="Number"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click"/>
        </div>
        <br />
        <br />
        <div>
             <asp:Label ID="lblStatus" Text="" runat="server"></asp:Label>
        </div>
        <br />
        <br />
        <div>
            <asp:GridView ID="gvEmployees" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
