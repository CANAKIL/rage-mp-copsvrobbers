const NativeUI = require("nativeui");
const Menu = NativeUI.Menu;
const UIMenuItem = NativeUI.UIMenuItem;
const UIMenuListItem = NativeUI.UIMenuListItem;
const UIMenuCheckboxItem = NativeUI.UIMenuCheckboxItem;
const UIMenuSliderItem = NativeUI.UIMenuSliderItem;
const BadgeStyle = NativeUI.BadgeStyle;
const Point = NativeUI.Point;
const ItemsCollection = NativeUI.ItemsCollection;
const Color = NativeUI.Color;
const ListItem = NativeUI.ListItem

mp.gui.cursor.visible = false;
mp.gui.chat.show(false);

//weatherUI

let timeItem = new UIMenuListItem(
	"Time",
	"Change the Time",
	new ItemsCollection(["Sunrise", "Day", "Sunset", "Night"])
);

let weatherItem = new UIMenuListItem(
	"Weather",
	"Change the Weather",
	new ItemsCollection(["Extrasunny", "Clear", "Clouds", "Smog", "Foggy", "Overcast", "Rain", "Thunder", "Clearing", "Neutral", "Snow", "Blizzard", "Snowlight", "XMas", "Halloween"])
);

const weatherUI = new Menu("Weather Changer", "Change Weather and Time", new Point(50, 50));
weatherUI.Close();
weatherUI.AddItem(timeItem);

weatherUI.AddItem(weatherItem);

weatherUI.ItemSelect.on(item => {
	if (item instanceof UIMenuListItem) {
		console.log(item.DisplayText, item.SelectedItem.Data);
		if(item.DisplayText == "Weather"){
			mp.events.callRemote("sysChangeWeather", item.SelectedItem.DisplayText);
		} else if (item.DisplayText == "Time"){
			console.log(item.DisplayText, item.SelectedItem.Data);
			mp.events.callRemote("sysChangeTime", item.SelectedItem.DisplayText);
		}
	} 
});

weatherUI.ListChange.on((item, listIndex) => {
	let weather = weatherItem.SelectedItem.DisplayText
	let time = timeItem.SelectedItem.DisplayText
	mp.events.callRemote("sysChangeTime", time);
	mp.events.callRemote("sysChangeWeather", weather);
});


mp.keys.bind(0x78, false, () => {
	if (weatherUI.Visible) weatherUI.Close();
	else weatherUI.Open();
});