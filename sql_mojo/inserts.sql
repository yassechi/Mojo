BEGIN TRANSACTION;

-- 1. NETTOYAGE PRÉVENTIF (Optionnel : vide les tables pour repartir à neuf)
-- DELETE FROM Messages; DELETE FROM Discussions; DELETE FROM Interventions; 
-- DELETE FROM Contrat_Accessoires; DELETE FROM Contrats; DELETE FROM Amortissements;
-- DELETE FROM Users; DELETE FROM Velos; DELETE FROM Organisations; DELETE FROM Accessoires;

-- 2. ORGANISATIONS
INSERT INTO [Organisations] ([Nom], [Code], [Adresse], [ContactEmail], [Actif])
VALUES 
(N'Vélo Libre Services', N'VLS_01', N'45 Avenue des Champs, Lyon', N'contact@vls.fr', 1),
(N'Eco-Rider Start', N'ECO_RS', N'10 Rue de la Paix, Nantes', N'admin@ecorider.io', 1),
(N'Cyclo Pro', N'CYC_PRO', N'2 bis Boulevard Magenta, Bordeaux', N'info@cyclopro.com', 1),
(N'Mojo Corporate', N'MOJO_HQ', N'1 Rue de la Pompe, Paris', N'hq@mojo.com', 1);

-- 3. ACCESSOIRES
INSERT INTO [Accessoires] ([Nom], [PrixAchat], [StockTotal])
VALUES 
(N'Casque City Reflect', 25.00, 100),
(N'Antivol Chaîne Acier', 45.00, 40),
(N'Panier Avant Amovible', 15.00, 30),
(N'Kit Lumière LED USB', 12.50, 60);

-- 4. VELOS
INSERT INTO [Velos] ([NumeroSerie], [Marque], [Modele], [PrixAchatMojo], [Statut])
VALUES 
(N'SN-ELEC-001', N'Moustache', N'Lundi 27.1', 2800.00, N'Loué'),
(N'SN-ELEC-002', N'Moustache', N'Samedi 28.3', 3100.00, N'Disponible'),
(N'SN-ROAD-003', N'Cannondale', N'Synapse', 1800.00, N'En Réparation'),
(N'SN-FOLD-004', N'Brompton', N'C-Line', 1600.00, N'Disponible');

-- 5. USERS (Utilisation de sous-requêtes pour l'OrganisationId)
INSERT INTO [Users] ([OrganisationId], [Nom], [Prenom], [Email], [MotDePasseHash], [Role], [TailleCm], [Actif])
VALUES 
((SELECT Id FROM Organisations WHERE Code = 'VLS_01'), N'Martin', N'Alice', N'alice.martin@vls.fr', N'HASH_1', N'Admin', 165, 1),
((SELECT Id FROM Organisations WHERE Code = 'VLS_01'), N'Bernard', N'Luc', N'luc.bernard@vls.fr', N'HASH_2', N'User', 182, 1),
((SELECT Id FROM Organisations WHERE Code = 'ECO_RS'), N'Petit', N'Chloé', N'chloe.p@ecorider.io', N'HASH_3', N'User', 170, 1),
((SELECT Id FROM Organisations WHERE Code = 'MOJO_HQ'), N'Yass', N'Admin', N'yass@mojo.com', N'HASH_4', N'Admin', 180, 1);

-- 6. AMORTISSEMENTS
INSERT INTO [Amortissements] ([VeloId], [DateDebut], [ValeurInitiale], [DureeMois], [ValeurResiduelleFinal])
SELECT Id, GETDATE(), PrixAchatMojo, 36, PrixAchatMojo * 0.15 FROM Velos;

-- 7. CONTRATS (Liaison dynamique par Email et Numéro de Série)
INSERT INTO [Contrats] ([CreatedById], [BeneficiaireId], [VeloId], [DateDebut], [DateFin], [LoyerMensuelHT], [StatutContrat])
VALUES 
(
  (SELECT Id FROM Users WHERE Email = 'yass@mojo.com'), 
  (SELECT Id FROM Users WHERE Email = 'luc.bernard@vls.fr'), 
  (SELECT Id FROM Velos WHERE NumeroSerie = 'SN-ELEC-001'), 
  '2025-01-01', '2025-12-31', 95.00, 1
),
(
  (SELECT Id FROM Users WHERE Email = 'yass@mojo.com'), 
  (SELECT Id FROM Users WHERE Email = 'chloe.p@ecorider.io'), 
  (SELECT Id FROM Velos WHERE NumeroSerie = 'SN-FOLD-004'), 
  '2025-02-01', '2026-02-01', 70.00, 1
),
(
  (SELECT Id FROM Users WHERE Email = 'alice.martin@vls.fr'), 
  (SELECT Id FROM Users WHERE Email = 'luc.bernard@vls.fr'), 
  (SELECT Id FROM Velos WHERE NumeroSerie = 'SN-ROAD-003'), 
  '2024-06-01', '2024-12-01', 55.00, 2
),
(
  (SELECT Id FROM Users WHERE Email = 'yass@mojo.com'), 
  (SELECT Id FROM Users WHERE Email = 'alice.martin@vls.fr'), 
  (SELECT Id FROM Velos WHERE NumeroSerie = 'SN-ELEC-002'), 
  '2025-03-01', '2026-03-01', 120.00, 1
);

-- 8. CONTRAT_ACCESSOIRES (Liaison sur les premiers contrats créés)
INSERT INTO [Contrat_Accessoires] ([ContratId], [AccessoireId], [Quantite])
SELECT TOP 1 c.Id, a.Id, 1 FROM Contrats c, Accessoires a WHERE a.Nom = N'Casque City Reflect' ORDER BY c.Id DESC;

-- 9. INTERVENTIONS
INSERT INTO [Interventions] ([VeloId], [ContratId], [DateIntervention], [Description], [Cout], [TypeIntervention])
VALUES 
((SELECT Id FROM Velos WHERE NumeroSerie = 'SN-ROAD-003'), NULL, GETDATE(), N'Révision de sortie de stock', 50.00, 1),
((SELECT Id FROM Velos WHERE NumeroSerie = 'SN-ELEC-001'), (SELECT TOP 1 Id FROM Contrats), GETDATE(), N'Crevaison roue arrière', 25.00, 2);

-- 10. DISCUSSIONS & MESSAGES
INSERT INTO [Discussions] ([ContratId], [Sujet], [DateCreation], [Statut])
VALUES ((SELECT TOP 1 Id FROM Contrats), N'Question sur l''assurance', GETDATE(), 1);

INSERT INTO [Messages] ([DiscussionId], [AuteurId], [Contenu], [DateEnvoi])
VALUES 
((SELECT TOP 1 Id FROM Discussions), (SELECT Id FROM Users WHERE Email = 'luc.bernard@vls.fr'), N'Est-ce que le vol est couvert ?', GETDATE()),
((SELECT TOP 1 Id FROM Discussions), (SELECT Id FROM Users WHERE Email = 'yass@mojo.com'), N'Oui, avec une franchise de 10%.', DATEADD(minute, 30, GETDATE()));

COMMIT;