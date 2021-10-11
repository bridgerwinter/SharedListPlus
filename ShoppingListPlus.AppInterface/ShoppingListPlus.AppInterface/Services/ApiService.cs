using System;
using System.Collections.Generic;
using System.Text;
using SharedListPlus.Library.Models;
using Newtonsoft.Json;
using ShoppingListPlus.AppInterface.ViewModels;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Net.Http;

namespace ShoppingListPlus.AppInterface.Services
{
	class ApiService
	{
		public static string RestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://bridger.cloud:5001/api" : "https://bridger.cloud:5001/api";
		private static HttpClient client = new HttpClient();
		public static HttpClient GetClient()
		{
			return client;
		}

		//Groups
		public static async Task<Group> GetGroups(long id)
		{
			string url = RestUrl + "Groups/" + id;
			try
			{
				string result = await GetClient().GetStringAsync(url);
				Group group = JsonConvert.DeserializeObject<Group>(result);
				return group;
			}
			catch (Exception)
			{
				Group group = new Group();
				return group;
				throw;
			}
		}
		public static async Task<HttpResponseMessage> PostGroup(Group group)
		{
			string url = RestUrl + "Groups/";
			// Serialize our concrete class into a JSON String
			string jsonChore = JsonConvert.SerializeObject(group);
			// Wrap our JSON inside a StringContent which then can be used by the HttpClient class
			StringContent httpContent = new StringContent(jsonChore, Encoding.UTF8, "application/json");
			HttpResponseMessage message = await GetClient().PostAsync(url, httpContent);
			return message;
		}
		public static async Task<HttpResponseMessage> PostFamilyShoppingList(Group group)
		{
			string url = RestUrl + "Groups/" + group.GroupId+ "/listitems/create/";
			string jsonChore = JsonConvert.SerializeObject(group.ListItems[group.ListItems.Count - 1]);
			StringContent httpContent = new StringContent(jsonChore, Encoding.UTF8, "application/json");
			HttpResponseMessage message = await GetClient().PostAsync(url, httpContent);
			return message;
		}
		public static async Task<HttpResponseMessage> UpdateGroup(Group group)
		{
			string url = RestUrl + "Groups/" + group.GroupId;
			//serial
			string jsonChore = JsonConvert.SerializeObject(group);
			StringContent httpContent = new StringContent(jsonChore, Encoding.UTF8, "application/json");
			HttpResponseMessage message = await GetClient().PutAsync(url, httpContent);
			return message;
		}

		//People
		public static async Task<HttpResponseMessage> UpdatePerson(Person person)
		{
			string url = RestUrl + "People/" + person.PersonId;
			//serialize bitch
			string jsonChore = JsonConvert.SerializeObject(person);
			//wrap our json
			StringContent httpContent = new StringContent(jsonChore, Encoding.UTF8, "application/json");
			HttpResponseMessage message = await GetClient().PutAsync(url, httpContent);
			return message;

		}
		public static async Task<HttpResponseMessage> PostPerson(Person person)
		{
			string url = RestUrl + "People/";
			// Serialize our concrete class into a JSON String
			string jsonChore = JsonConvert.SerializeObject(person);
			// Wrap our JSON inside a StringContent which then can be used by the HttpClient class
			StringContent httpContent = new StringContent(jsonChore, Encoding.UTF8, "application/json");
			HttpResponseMessage message = await GetClient().PostAsync(url, httpContent);
			return message;
		}
		public static async Task<Person> GetPerson(long id)
		{
			string url = RestUrl + "People/" + id;
			try
			{
				string result = await GetClient().GetStringAsync(url);
				Person person = JsonConvert.DeserializeObject<Person>(result);
				return person;

			}
			catch (Exception)
			{
				Person person = new Person();
				return person;
				throw;
			}
		}

		//ListItems
		public static async Task<HttpResponseMessage> DeleteShoppingItem(long id)
		{
			string url = RestUrl + "ListItems/" + id;
			HttpResponseMessage message = await GetClient().DeleteAsync(url);
			return message;
		}
	}
}
