using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_Products_product_id",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK_carts_users_user_id",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK_orderdetails_Products_product_id",
                table: "orderdetails");

            migrationBuilder.DropForeignKey(
                name: "FK_orderdetails_orders_order_id",
                table: "orderdetails");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_user_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_users_user_id",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_Products_product_id",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_users_user_id",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_viewhistory_Products_product_id",
                table: "viewhistory");

            migrationBuilder.DropForeignKey(
                name: "FK_viewhistory_users_user_id",
                table: "viewhistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reviews",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_promocodes",
                table: "promocodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payments",
                table: "payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderdetails",
                table: "orderdetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_carts",
                table: "carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_viewhistory",
                table: "viewhistory");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "reviews",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "promocodes",
                newName: "PromoCodes");

            migrationBuilder.RenameTable(
                name: "payments",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "orderdetails",
                newName: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "carts",
                newName: "Carts");

            migrationBuilder.RenameTable(
                name: "viewhistory",
                newName: "ViewHistories");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "shipping_address",
                table: "Users",
                newName: "ShippingAddress");

            migrationBuilder.RenameColumn(
                name: "registration_date",
                table: "Users",
                newName: "RegistrationDate");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Reviews",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Reviews",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "review_text",
                table: "Reviews",
                newName: "ReviewText");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "Reviews",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "review_id",
                table: "Reviews",
                newName: "ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_user_id",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_product_id",
                table: "Reviews",
                newName: "IX_Reviews_ProductId");

            migrationBuilder.RenameColumn(
                name: "discount",
                table: "PromoCodes",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "PromoCodes",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "promo_name",
                table: "PromoCodes",
                newName: "PromoName");

            migrationBuilder.RenameColumn(
                name: "end_date",
                table: "PromoCodes",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "promo_id",
                table: "PromoCodes",
                newName: "PromoId");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Payments",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Payments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "payment_status",
                table: "Payments",
                newName: "PaymentStatus");

            migrationBuilder.RenameColumn(
                name: "payment_date",
                table: "Payments",
                newName: "PaymentDate");

            migrationBuilder.RenameColumn(
                name: "payment_id",
                table: "Payments",
                newName: "PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_payments_user_id",
                table: "Payments",
                newName: "IX_Payments_UserId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "order_status",
                table: "Orders",
                newName: "OrderStatus");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_user_id",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "OrderDetails",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "unit_price",
                table: "OrderDetails",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "OrderDetails",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "OrderDetails",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "detail_id",
                table: "OrderDetails",
                newName: "OrderDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_orderdetails_product_id",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_orderdetails_order_id",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "Carts",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Carts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "Carts",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "cart_id",
                table: "Carts",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_carts_user_id",
                table: "Carts",
                newName: "IX_Carts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_carts_product_id",
                table: "Carts",
                newName: "IX_Carts_ProductId");

            migrationBuilder.RenameColumn(
                name: "view_date",
                table: "ViewHistories",
                newName: "ViewDate");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "ViewHistories",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "ViewHistories",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "history_id",
                table: "ViewHistories",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_viewhistory_user_id",
                table: "ViewHistories",
                newName: "IX_ViewHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_viewhistory_product_id",
                table: "ViewHistories",
                newName: "IX_ViewHistories_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromoCodes",
                table: "PromoCodes",
                column: "PromoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "OrderDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ViewHistories",
                table: "ViewHistories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_UserId",
                table: "Payments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewHistories_Products_ProductId",
                table: "ViewHistories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewHistories_Users_UserId",
                table: "ViewHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_UserId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ViewHistories_Products_ProductId",
                table: "ViewHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ViewHistories_Users_UserId",
                table: "ViewHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromoCodes",
                table: "PromoCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_OrderId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ViewHistories",
                table: "ViewHistories");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "reviews");

            migrationBuilder.RenameTable(
                name: "PromoCodes",
                newName: "promocodes");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "payments");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "orders");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "orderdetails");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "carts");

            migrationBuilder.RenameTable(
                name: "ViewHistories",
                newName: "viewhistory");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress",
                table: "users",
                newName: "shipping_address");

            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "users",
                newName: "registration_date");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "users",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "users",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "reviews",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "reviews",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ReviewText",
                table: "reviews",
                newName: "review_text");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "reviews",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "reviews",
                newName: "review_id");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "reviews",
                newName: "IX_reviews_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ProductId",
                table: "reviews",
                newName: "IX_reviews_product_id");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "promocodes",
                newName: "discount");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "promocodes",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "PromoName",
                table: "promocodes",
                newName: "promo_name");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "promocodes",
                newName: "end_date");

            migrationBuilder.RenameColumn(
                name: "PromoId",
                table: "promocodes",
                newName: "promo_id");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "payments",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "payments",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "payments",
                newName: "payment_status");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "payments",
                newName: "payment_date");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "payments",
                newName: "payment_id");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_UserId",
                table: "payments",
                newName: "IX_payments_user_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "orders",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "orders",
                newName: "total_amount");

            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "orders",
                newName: "order_status");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "orders",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "orders",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "orders",
                newName: "IX_orders_user_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "orderdetails",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "orderdetails",
                newName: "unit_price");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "orderdetails",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "orderdetails",
                newName: "order_id");

            migrationBuilder.RenameColumn(
                name: "OrderDetailId",
                table: "orderdetails",
                newName: "detail_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductId",
                table: "orderdetails",
                newName: "IX_orderdetails_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "orderdetails",
                newName: "IX_orderdetails_order_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "carts",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "carts",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "carts",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "carts",
                newName: "cart_id");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_UserId",
                table: "carts",
                newName: "IX_carts_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ProductId",
                table: "carts",
                newName: "IX_carts_product_id");

            migrationBuilder.RenameColumn(
                name: "ViewDate",
                table: "viewhistory",
                newName: "view_date");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "viewhistory",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "viewhistory",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "viewhistory",
                newName: "history_id");

            migrationBuilder.RenameIndex(
                name: "IX_ViewHistories_UserId",
                table: "viewhistory",
                newName: "IX_viewhistory_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_ViewHistories_ProductId",
                table: "viewhistory",
                newName: "IX_viewhistory_product_id");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reviews",
                table: "reviews",
                column: "review_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_promocodes",
                table: "promocodes",
                column: "promo_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payments",
                table: "payments",
                column: "payment_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "order_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderdetails",
                table: "orderdetails",
                column: "detail_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_carts",
                table: "carts",
                column: "cart_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_viewhistory",
                table: "viewhistory",
                column: "history_id");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_Products_product_id",
                table: "carts",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carts_users_user_id",
                table: "carts",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderdetails_Products_product_id",
                table: "orderdetails",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderdetails_orders_order_id",
                table: "orderdetails",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_user_id",
                table: "orders",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_users_user_id",
                table: "payments",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_Products_product_id",
                table: "reviews",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_users_user_id",
                table: "reviews",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_viewhistory_Products_product_id",
                table: "viewhistory",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_viewhistory_users_user_id",
                table: "viewhistory",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
