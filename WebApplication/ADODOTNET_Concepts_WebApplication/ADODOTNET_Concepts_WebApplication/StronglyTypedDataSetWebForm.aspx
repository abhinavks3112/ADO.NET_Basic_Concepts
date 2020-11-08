<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StronglyTypedDataSetWebForm.aspx.cs" Inherits="ADODOTNET_Concepts_WebApplication.StronglyTypedDataSetWebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtTextToSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Button" />
            <br />
            <br />
            <asp:GridView ID="gvStudents" runat="server">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
