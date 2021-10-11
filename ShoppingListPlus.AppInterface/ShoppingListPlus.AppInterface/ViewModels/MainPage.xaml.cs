using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ShoppingListPlus.AppInterface.ViewModels;
using SharedListPlus.Library.Models;
using System.Windows.Input;

namespace ShoppingListPlus.AppInterface.ViewModels
{
	public partial class MainPage : ContentPage
	{
		public List<ListItem> listOfItems = new List<ListItem>();
		public MainPage()
		{
			InitializeComponent();
		
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			ListItem item1 = new ListItem("Eggs");
			ListItem item2 = new ListItem("Bread");
			ListItem item3 = new ListItem("Cheese");
			ListItem item4 = new ListItem("Milk");
			ListItem item5 = new ListItem("Soap");
			ListItem item6 = new ListItem("Toilet Paper");

			listOfItems.Add(item1);
			listOfItems.Add(item2);
			listOfItems.Add(item3);
			listOfItems.Add(item4);
			listOfItems.Add(item5);
			listOfItems.Add(item6);

			listOfItems.OrderBy(X => X.Item);
			uiList.ItemsSource = listOfItems;
		}
		private void uiList_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			ListItem current = uiList.SelectedItem as ListItem;
			int index = listOfItems.IndexOf(current);
			if (current.Item.Contains($"{current.Item[0]}\u0336"))
			{
				string swap = current.StrikethroughItem;
				current.StrikethroughItem = current.Item;
				current.Item = swap;
			}
			else
			{
				string swap = current.Item;
				current.Item = current.StrikethroughItem;
				current.StrikethroughItem = swap;

			}
			uiList.ItemsSource = listOfItems.Where(x => x.Item != null).OrderBy(x => x.Item);
		}

		private void toolbarAdd_Clicked(object sender, EventArgs e)
		{

		}

		private void toolbarEdit_Clicked(object sender, EventArgs e)
		{
			/*
			if (uiList.SelectionMode == SelectionMode.Multiple)
			{
				this.uiList.SelectionMode = SelectionMode.Multiple;
				uiList.MultiSe = null;
				uiList.BackgroundColor = Color.Default;
				labelIsEditing.IsVisible = false;
			}
			else
			{
				uiList.MultiSelect = true;
				uiList.SelectedItem = null;
				labelIsEditing.IsVisible = true;
				uiList.BackgroundColor = Color.LightGray;
			}
			*/
		}
		public void OnEnterPressed(object sender, EventArgs e)
		{
			ListItem item = new ListItem(entryNewItem.Text);
			listOfItems.Add(item);
			entryNewItem.Text = null;
			uiList.ItemsSource = listOfItems.Where(x => x.Item != null);
			//add to the list;	
		}
		private void toolbarGroup_Clicked(object sender, EventArgs e)
		{

		}

		private void buttonSortAlpha_Clicked(object sender, EventArgs e)
		{
			uiList.ItemsSource = listOfItems.Where(x => x.Item != null).OrderBy(x => x.Item);
			listOfItems.OrderBy(x => x.Item);
		}
		

		private void search_TextChanged(object sender, TextChangedEventArgs e)
		{

			uiList.ItemsSource = listOfItems.Where(x => x.Item.ToLower().Contains(search.Text.ToLower()));

		}

	}
}
