import {
  Component,
  ContentChildren,
  TemplateRef,
  QueryList,
  ViewChildren,
  OnInit,
} from "@angular/core";
import { AuthorizeService } from "../../api-authorization/authorize.service";

@Component({
  selector: "app-sidebar-menu",
  templateUrl: "./sidebar-menu.component.html",
  styleUrls: ["./sidebar-menu.component.scss"],
})
export class SidebarMenuComponent implements OnInit {
  constructor(private authService: AuthorizeService) {}

  ngOnInit(): void {
    this.authService.getUser().subscribe((user) => {
      if (user) {
        this.permissions =
          user["http://schemas.eridirect.com/ws/claims/2018/02/permission"] ||
          [];
        this.admin =
          user[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
          ] == "Administrator";
      }
    });

    fetch("/_config/").then((response) => {
      response.json().then((result) => {
        this.baseUrl = result.v1Uri;
      });
    });
  }

  admin: boolean = false;
  permissions: string[] = [];

  baseUrl: string = "";
  isExpanded = false;
  menu = [
    {
      name: "Home",
      text: "Home",
      title: "Home",
      path: "/",
    },
   
    {
      name: "Orders",
      text: "Orders",
      title: "Orders",
      subMenus: [
        {
          name: "OrderView",
          text: "Search",
          title: "Order Search / View",
          path: "/orders/index",
        },
        {
          name: "OrderView",
          text: "Recurring",
          title: "Recurring Orders Search / View",
          path: "/orders/recurring/index",
        },
        {
          name: "OrderCreate",
          text: "Create",
          title: "Order Create",
          path: "/orders/create",
        },
        {
          name: "ExternalImport",
          text: "External Imports",
          title: "External Import",
          path: "/orders/externalimport/index",
        },
        {
          name: "OrderAdmin",
          text: "Administration",
          title: "Order Administration",
          path: "/orders/admin/admin.aspx",
        },
      ],
    },
    {
      name: "Services",
      text: "Services",
      title: "Services",
      subMenus: [
        {
          name: "CreateServiceOrder",
          text: "Create Order",
          title: "Create Service Order",
          path: "/services/createserviceshipment.aspx",
        },
        {
          name: "ServiceSearch",
          text: "Search",
          title: "Service Search",
          path: "/services/",
        },
        {
          name: "PackageAdmin",
          text: "Administration",
          title: "Administration",
          path: "/services/packages.aspx",
        },
      ],
    },
    {
      name: "Logistics",
      text: "Logistics",
      title: "Logistics",
      subMenus: [
        {
          name: "SearchFreightReview",
          text: "Freight Billing Approval A",
          title: "Approve Freight Billing B",
          path: "/logistics/freight/review/index.aspx",
        },
        
      ],
    },

   
   
    
   
    {
      name: "Reports",
      text: "Reports",
      title: "Reports",
      path: "/reports",
    },
    {
      name: "Sales",
      text: "Sales",
      title: "Sales",
      subMenus: [
        {
          name: "Accounts",
          text: "Accounts",
          title: "Add/View accounts",
          path: "/sales/accounts/index.aspx",
        },
        {
          name: "SalesRepGoals",
          text: "Goals",
          title: "Add/Edit goals for a selected sales rep",
          path: "/sales/goals/index.aspx",
        },
      ],
    },
   
    {
      name: "Accounting",
      text: "Accounting",
      title: "Accounting",
      subMenus: [
        {
          name: "Invoicing",
          text: "Invoicing",
          title: "Create, Edit or View Invoices",
          path: "/invoicing",
        },
        {
          name: "InvoicingApproval",
          text: "Invoice Approval",
          title: "Invoice Approval",
          path: "/invoicing/approval.aspx",
        },
        {
          name: "Payments",
          text: "Payments",
          title: "Manage incoming or outgoing payments",
          path: "/invoicing/payments",
        },
        {
          name: "NAVRecords",
          text: "NAV Records",
          title: "NAV Records",
          path: "/invoicing/nav.aspx",
        },
        {
          name: "NAVTests",
          text: "NAV Testing",
          title: "NAV Testing",
          path: "/invoicing/navtests.aspx",
        },
        {
          name: "PaymentsMail",
          text: "Mail Payments",
          title: "Mail payments in the system",
          path: "/invoicing/payments/mail.aspx",
        },
      ],
    },
    {
      name: "Compliance",
      text: "Compliance",
      title: "Compliance",
      subMenus: [
        {
          name: "CACEWClaim",
          text: "CEW Claims",
          title: "Create or Edit California CEW Claims.",
          path: "/compliance/ca_cewclaims",
        },
        {
          name: "CompReviewSearch",
          text: "Search",
          title: "Search for Compliance Review",
          path: "/compliance/review",
        },
        {
          name: "ComplianceFormDashboard",
          text: "Form Dashboard",
          title: "Form Dashboard",
          path: "/compliance/forms/dashboard.aspx",
        },
        {
          name: "ViewFormResponse",
          text: "Form Responses",
          title: "Form Responses",
          path: "/compliance/forms/response/index.aspx",
        },
        {
          name: "ComplianceFormAdmin",
          text: "Form Admin",
          title: "Form Administration",
          path: "/compliance/forms/index.aspx",
        },
      ],
    },
    {
      name: "Admin",
      text: "System Admin",
      title: "System Admin",
      subMenus: [
        {
          name: "Sites",
          text: "Sites",
          title: "Manage the information for each ERI Location",
          path: "/admin/sites.aspx",
        },
        {
          name: "ReceivingTypes",
          text: "Receiving Types",
          title: "Add New Receiving Types",
          path: "/receivingtypes",
        },
        {
          name: "Settings",
          text: "My Settings",
          title: "Change Your Personal Settings",
          path: "/settings",
        },
        {
          name: "Employees",
          text: "Employees",
          title: "Manage Employees",
          path: "/employees",
        },
        {
          name: "Projects",
          text: "Projects",
          title: "Projects",
          path: "/projects",
        },
        {
          name: "Tasks",
          text: "Tasks",
          title: "Tasks",
          path: "/tasks",
        },
        {
          name: "ComplianceFormAdmin",
          text: "Forms",
          title: "Forms",
          path: "/admin/forms/index.aspx",
        },
      ],
    },
    {
      name: "Logs",
      text: "System Logs",
      title: "View the system logs",
      subMenus: [
        {
          name: "Exceptions",
          text: "Exceptions",
          title:
            "View a log of all system errors and the users that encountered them",
          path: "/logs/exceptions.aspx",
        },
        {
          name: "Changes",
          text: "Changes",
          title: "View a log of all changes to the system and who made them",
          path: "/logs/changes.aspx",
        },
        {
          name: "LoggedIn",
          text: "Logged In",
          title: "View a list of all currently logged in users",
          path: "/logs/logins.aspx",
        },
      ],
    },
    {
      name: "ReleaseNotes",
      text: "Release Notes",
      title: "Release Notes",
      path: "/ReleaseNotes",
    },
    {
      name: "ProductCatalog",
      text: "Product Catalog (Beta)",
      title: "Product Catalog",
      subMenus: [
        {
          name: "Categories",
          text: "Categories",
          title: "Manage product catalog categories",
          path: "/catalog/category",
        },
        {
          name: "Features",
          text: "Features",
          title: "Manage product catalog features",
          path: "/catalog/feature",
        },
        {
          name: "Manufacturers",
          text: "Manufacturers",
          title: "Manage product catalog manufacturers",
          path: "/catalog/manufacturer",
        },
        {
          name: "Products",
          text: "Products",
          title: "Manage product catalog products",
          path: "/catalog/product",
        },
      ],
    },
  ];
  expansion: any = {};

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  kebabCase(string: string): string {
    return string
      .replace(/([a-z0-9]|(?=[A-Z]))([A-Z])/g, "$1-$2")
      .toLowerCase();
  }
}
