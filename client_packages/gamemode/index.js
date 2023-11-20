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

let pickedSide = null;

let teamItem = new UIMenuListItem(
	"Side",
	"Choose your side",
	new ItemsCollection(["Cops", "Robbers"])
);

const chaseUI = new Menu("Chase is starting...", "Pick your side!", new Point(50, 50));
chaseUI.Close();
chaseUI.AddItem(teamItem);

chaseUI.ItemSelect.on(item => {
    if (item instanceof UIMenuListItem) {
        pickedSide = item.SelectedItem.DisplayText;
        chaseUI.Close();
        mp.gui.chat.show(true);
    }
});

mp.events.add('startRaceProcedure', (player) => {
    setTimeout(() => {
        mp.events.callRemote("pickedSide", pickedSide);
    }, 15000)
    chaseUI.Open();
});

let toggle = false;
mp.events.add('toggleDisableCarControls', () => {
    toggle = true;
});

mp.events.add('render', () => {
    mp.game.controls.disableControlAction(32, 75, toggle);
    mp.game.controls.disableControlAction(32, 71, toggle);
    mp.game.controls.disableControlAction(32, 72, toggle);
});