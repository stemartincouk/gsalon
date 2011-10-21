using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	VBox vboxMain = new VBox (false,10);
	HBox hbTop = new HBox (false,1);
	HPaned verticalPane = new HPaned ();
	ScrolledWindow scrollWindowLeft = new ScrolledWindow ();
	ScrolledWindow scrollWindowRight = new ScrolledWindow ();
	Viewport viewPortLeft = new Viewport ();
	Viewport viewPortRight = new Viewport ();
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Application.Init ();
	
			this.Resize(640,480);
			//menu bar very top
			MenuBar mb = new MenuBar ();
			Menu fileMenu = new Menu ();
			MenuItem menuItem = new MenuItem ("_File");
			menuItem.Submenu = fileMenu;
			mb.Append(menuItem);
			MenuItem menuFileQuit = new MenuItem("Quit");
			fileMenu.Append(menuFileQuit);
			vboxMain.PackStart(mb,false,false,0);
			
			//toolbar
			Toolbar tbTop = new Toolbar ();
			//toolbutton Staff
			ToolButton tbStaff = new ToolButton (Gtk.Stock.OrientationPortrait);
			tbStaff.Label="Staff";
			tbStaff.IsImportant=true;
			tbStaff.Clicked += HandleTbStaffClicked;
			tbTop.Insert(tbStaff,0);
			//toolbutton Clients 
			ToolButton tbClients = new ToolButton (Gtk.Stock.About);
			tbClients.Label="Clients";
			tbClients.IsImportant=true;
			tbClients.Clicked+= HandleTbClientsClicked;
			tbTop.Insert(tbClients,1);
			//media bar
			Label lbMediaTemp = new Label ();
			lbMediaTemp.Text="Media holder";
			lbMediaTemp.Show();
			//pack the toolbar and media bar in the top hbox//
			hbTop.PackStart(tbTop);
			hbTop.PackStart(lbMediaTemp);
			//pack the top hbox in the main vbox
			vboxMain.PackStart(hbTop,false,false,1);
			// horizontal pane
			
			verticalPane.Pack1(scrollWindowLeft,false,false);
			verticalPane.Pack2(scrollWindowRight,false,false);
			vboxMain.PackStart(verticalPane);
			scrollWindowLeft.Add(viewPortLeft);
			
			scrollWindowRight.Add(viewPortRight);
			Label lbMain = new Label ();
			lbMain.Text= "main";
			viewPortRight.Add(lbMain);
			verticalPane.ShowAll();
			//status bar very bottom
			Statusbar sb = new Statusbar ();
			vboxMain.PackStart(sb,false,false,1);
			
			

			
			
			
			
			
			
			
			this.Add(vboxMain);
			//hb1.Add(tbTop);
			this.ShowAll ();
		Build ();
	}

	void HandleTbClientsClicked (object sender, EventArgs e)
	{
		Label lbClients =new Label ();
		lbClients.Text ="Im the clients bit";
		viewPortLeft.Remove(viewPortLeft.Child);
		viewPortLeft.Add(lbClients);
		viewPortLeft.Show();
		lbClients.Show();
		Console.WriteLine("Clients Clicked");
		
		
		
	}

	void HandleTbStaffClicked (object sender, EventArgs e)
	{
		ToolButton tb =(ToolButton)sender;
	
		viewPortLeft.Remove(viewPortLeft.Child);
		Label lbStaff = new Label ();
		lbStaff.Text="im the staff bit";
		
		
		viewPortLeft.Add(lbStaff);
		viewPortLeft.Show();
		lbStaff.Show();
		
		Console.WriteLine("staff clicked");
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
