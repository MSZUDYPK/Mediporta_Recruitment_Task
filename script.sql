CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Populations" (
    "Id" uuid NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Populations" PRIMARY KEY ("Id")
);

CREATE TABLE "SimplifiedTags" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Share" double precision NOT NULL,
    "PopulationId" uuid,
    CONSTRAINT "PK_SimplifiedTags" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_SimplifiedTags_Populations_PopulationId" FOREIGN KEY ("PopulationId") REFERENCES "Populations" ("Id")
);

CREATE INDEX "IX_SimplifiedTags_PopulationId" ON "SimplifiedTags" ("PopulationId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240330214438_InitialMigration', '8.0.3');

COMMIT;

