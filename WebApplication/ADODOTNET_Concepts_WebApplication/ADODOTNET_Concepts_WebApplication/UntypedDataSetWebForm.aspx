<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UntypedDataSetWebForm.aspx.cs" Inherits="ADODOTNET_Concepts_WebApplication.StronglyTypedDataSetWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:TextBox ID="txtNameToSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
            </div>
            <br />
            <br />
            <div>
                <asp:GridView ID="gvStudents" runat="server">
                </asp:GridView>
            </div>            
        </div>
    </form>
</body>
</html>
