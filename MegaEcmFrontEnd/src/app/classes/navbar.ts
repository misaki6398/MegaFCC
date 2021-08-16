export class Navbar {
  title: string;
  menuItems: MenuItem[];
}

export class MenuItem {
  title: string;
  url: string;
  icon: string;
  active: boolean;
  subItems: Subitem[];
}

export class Subitem {
  title: string;
  url: string;
  active: boolean;
}
