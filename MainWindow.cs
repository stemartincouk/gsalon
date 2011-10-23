using System;
using System.Collections.Generic;
using Gtk;
using gsalon;

public partial class MainWindow: Gtk.Window
{	
	VBox vboxMain = new VBox (false,10);
	HBox hbTop = new HBox (false,1);
	HPaned verticalPane = new HPaned ();
	ScrolledWindow scrollWindowLeft = new ScrolledWindow ();
	ScrolledWindow scrollWindowRight = new ScrolledWindow ();
	Viewport viewPortLeft = new Viewport ();
	Viewport viewPortRight = new Viewport ();
	Entry tbSearch ;
	HBox hboxSearch;
	VBox vboxVpLeft;
	TreeView tvResults = new TreeView ();
	
	
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
			verticalPane.Position=200;
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

	void HandleTbSearchKeyReleaseEvent (object o, KeyReleaseEventArgs args)
	{
		//get results live on key up
		staff staffMember = new staff();
		staffMember.fName = "ted";
		List<staff> searchResults = new List<staff> ();
		searchResults = staffMember.search_staff(tbSearch.Text);
		Console.WriteLine("Search Results found: "+searchResults.Count.ToString());
		//staffMember.get_staff_member();
		foreach(staff sr in searchResults)
		{
			
			Console.WriteLine(sr.fName+" "+sr.lName);	
		}
	}

	void HandleTbClientsClicked (object sender, EventArgs e)
	{
		viewPortRight.Remove(viewPortLeft.Child);
		Label lbClients =new Label ();
		lbClients.Text ="Im the clients bit";
		viewPortLeft.Remove(viewPortLeft.Child);
		viewPortLeft.Add(lbClients);
		viewPortLeft.Show();
		lbClients.Show();
		Console.WriteLine("Clients Clicked");
		staff s = new staff ();
	    List<staff> staffList = new List<staff>();
		staffList= s.get_all_staff_members();
		foreach(staff sm in staffList)
		{
			Console.WriteLine(sm.fName);
		}
		
		
		
	}

	void HandleTbStaffClicked (object sender, EventArgs e)
	{
		//set up search input
		tbSearch=new Entry ();
		tbSearch.KeyReleaseEvent+= HandleTbSearchKeyReleaseEvent;
		viewPortLeft.Remove(viewPortLeft.Child);
		
		hboxSearch = new HBox ();
		vboxVpLeft = new VBox ();
		
		Button btSearchGo = new Button ();
		btSearchGo.Clicked+= HandleBtSearchGoClicked;
		
		btSearchGo.Label="Go";
		viewPortLeft.Remove(viewPortLeft.Child);
		Label lbStaff = new Label ();
		lbStaff.Text="im the staff bit";
		
		
		viewPortLeft.Add(vboxVpLeft);
		hboxSearch.PackStart(tbSearch,true,true,1);
		hboxSearch.PackStart(btSearchGo,false,false,1);
		vboxVpLeft.PackStart(hboxSearch,false,false,1);
		viewPortLeft.Show();
		lbStaff.Show();
		tbSearch.Show();
		vboxVpLeft.ShowAll();
		
		Console.WriteLine("staff clicked");
	}

	void HandleBtSearchGoClicked (object sender, EventArgs e)
	{
		tvResults.Destroy();
		tvResults = new TreeView ();
		Console.WriteLine("Searched: "+tbSearch.Text);
		staff staffMember = new staff();
		staffMember.fName = "ted";
		List<staff> searchResults = new List<staff> ();
		searchResults = staffMember.search_staff(tbSearch.Text);
		Console.WriteLine("Search Results found: "+searchResults.Count.ToString());
		//staffMember.get_staff_member();
		foreach(staff sr in searchResults)
		{
			
			Console.WriteLine(sr.fName+" "+sr.lName);	
		}
		
		//Console.WriteLine(staffMember.fName);
		Gtk.ListStore searchResultsStore  = new Gtk.ListStore (typeof (string));
		searchResultsStore.AppendValues ("Gerry");
		Gtk.CellRendererText staffResult = new Gtk.CellRendererText ();
		
		tvResults.Model = searchResultsStore;
		
		vboxVpLeft.PackStart(tvResults);
		TreeViewColumn tvcName = new TreeViewColumn ();
		tvcName.Title="Name";
		tvcName.PackStart(staffResult,true);
		tvResults.AppendColumn(tvcName);
		tvcName.AddAttribute (staffResult, "text", 0);
		tvResults.Show();
		vboxVpLeft.PackStart(tvResults);
		
		
	
		
		
	
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
