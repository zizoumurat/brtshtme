export interface MenuItemModel {
    code: string;
    name: string;
    icon: string;
    appUrl: string;
    label: string;
    isAdmin:boolean;
    items: MenuItemModel[];
    bttnRef: number;
}


