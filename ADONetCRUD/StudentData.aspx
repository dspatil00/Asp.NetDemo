<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentData.aspx.cs" Inherits="ADONetCRUD.StudentData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Student Portal</title>

	<link href="Content/bootstrap.rtl.min.css" rel="stylesheet" />
	<script src="Scripts/bootstrap.min.js"></script>
	<script src="Scripts/jquery-3.6.0.min.js"></script>

</head>
<body>
	<form id="form1" runat="server">
		<div class="container">

			<h1>Student Create Form</h1>

			<div>
				<div class="form-group">
					<div class="col-md-2">
						<asp:Label ID="Label5" runat="server" Text="RollNumber" CssClass="Control lable"></asp:Label>
					</div>
					<div class="col-md-4">
						<asp:TextBox ID="txtRollNumber" runat="server" CssClass="form-control"></asp:TextBox>
						<asp:Button ID="btnLoad" runat="server" Text="Load Student" CssClass="form-control btn btn-info" OnClick="btnLoad_Click" />
					</div>
				</div>

				<div class="form-group">
					<div class="col-md-2">
						<asp:Label ID="Label1" runat="server" Text="Name" CssClass="Control lable"></asp:Label>
					</div>
					<div class="col-md-4">
						<asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
					</div>
				</div>

				<div class="form-group">
					<div class="col-md-2">
						<asp:Label ID="Label2" runat="server" Text="Age" CssClass="Control lable"></asp:Label>
					</div>
					<div class="col-md-4">
						<asp:TextBox ID="txtAge" runat="server" CssClass="form-control"></asp:TextBox>
					</div>
				</div>

				<div class="form-group">
					<div class="col-md-2">
						<asp:Label ID="Label3" runat="server" Text="Email" CssClass="Control lable"></asp:Label>
					</div>
					<div class="col-md-4">
						<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
					</div>
				</div>

				<div class="form-group">
					<div class="col-md-2">
						<asp:Label ID="Label4" runat="server" Text="Date of Birth" CssClass="Control lable"></asp:Label>
					</div>
					<div class="col-md-4">
						<asp:TextBox ID="txtDateOfBirth" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
					</div>
				</div>
				<div>
					<br />
					<div class="form-group">
						<div class="col-md-4">
							<asp:Button ID="btnCreate" CssClass="form-control btn btn-primary" runat="server" Text="Create" OnClick="btnCreate_Click" />

							<asp:Button ID="btnClear" CssClass="form-control btn btn-warning" runat="server" Text="Clear" OnClick="btnClear_Click" />

							<asp:Button ID="btnUpdate" CssClass="form-control btn btn-secondary" runat="server" Text="Update" OnClick="btnUpdate_Click" />

							<asp:Button ID="btnDelete" CssClass="form-control btn btn-danger" runat="server" Text="Delete" OnClick="btnDelete_Click" />

						</div>
					</div>
				</div>
				<div>
					<br />
					<div class="form-group">
						<div class="col-md-4">
							<asp:Label ID="lblMessage" CssClass="Control-lable" Font-Bold="true" Font-Size="Large" runat="server"></asp:Label>
						</div>
					</div>
				</div>
			</div>
			
				<div class="container">
					<div>
						<br />
						<div class="form-group">
							<div class="col-md-4">
								<asp:Button ID="btnBackup" runat="server" Text="Backup Student" OnClick="btnBackup_Click" CssClass="btn btn-light" />
								<asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" CssClass="btn btn-light" />

							</div>
						</div>
					</div>
				</div>


				<h1>Students List</h1>

				<asp:GridView ID="gvStudents" runat="server" CssClass="table table border table-hover"></asp:GridView>

			

		</div>
	</form>
</body>
</html>
